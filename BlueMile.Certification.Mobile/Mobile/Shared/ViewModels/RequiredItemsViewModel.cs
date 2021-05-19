using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    [QueryProperty("CurrentBoatId", "boatId")]
    public class RequiredItemsViewModel : BaseViewModel
    {
        #region Instance Properties

        public string CurrentBoatId
        {
            get { return this.currentBoatId; }
            set
            {
                this.currentBoatId = Uri.UnescapeDataString(value);
                this.GetBoatItems().ConfigureAwait(false);
            }
        }

        public ObservableCollection<ItemMobileModel> RequiredItems
        {
            get { return this.requiredItems; }
            set
            {
                if (this.requiredItems != value)
                {
                    this.requiredItems = value;
                    this.OnPropertyChanged(nameof(this.RequiredItems));
                }
            }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    this.OnPropertyChanged(nameof(this.IsRefreshing));
                }
            }
        }

        public ItemMobileModel SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged(nameof(this.SelectedItem));

                    this.OpenItemDetail();
                }
            }
        }

        public ICommand AddItemCommand
        {
            get;
            private set;
        }

        public ICommand ViewRequiredItemsCommand
        {
            get;
            private set;
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }

        public ICommand SyncItemsCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public RequiredItemsViewModel()
        {
            this.Title = "Equipment List";
            this.InitCommands();
            if (!String.IsNullOrWhiteSpace(this.CurrentBoatId))
            {
                this.GetBoatItems().ConfigureAwait(false);
            }

            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.AddItemCommand = new Command(async () =>
            {
                await CreateNewItem().ConfigureAwait(false);
            });
            this.ViewRequiredItemsCommand = new Command(async () =>
            {
                await UserDialogs.Instance.AlertAsync(await RequirementValidationService.GetRequiredItems(Guid.Parse(this.CurrentBoatId)).ConfigureAwait(false)).ConfigureAwait(false);
            });
            this.RefreshCommand = new Command(async () =>
            {
                this.IsRefreshing = true;
                await this.GetBoatItems().ConfigureAwait(false);
                this.IsRefreshing = false;
            });
            this.SyncItemsCommand = new Command(async () =>
            {
                await this.SyncBoatsToServer();
            });
        }

        private async Task CreateNewItem()
        {
            try
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.itemNewRoute}?boatId={this.CurrentBoatId}").ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "New Item Error").ConfigureAwait(false);
            }
        }

        private async Task GetBoatItems()
        {
            if (this.dataService == null)
            {
                this.dataService = new DataService();
            }

            this.RequiredItems = new ObservableCollection<ItemMobileModel>(await this.dataService.FindItemsByBoatIdAsync(Guid.Parse(this.CurrentBoatId)).ConfigureAwait(false));
        }

        private async void OpenItemDetail()
        {
            UserDialogs.Instance.ShowLoading("Loading...");
            await Shell.Current.GoToAsync($"{Constants.itemDetailRoute}?itemId={this.SelectedItem.Id}").ConfigureAwait(false);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async Task SyncBoatsToServer()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Syncing...");

                foreach (var item in this.RequiredItems)
                {
                    await this.SaveItemDetails(item);
                }

                await this.GetBoatItems();

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Sync Error");
            }
        }

        private async Task SaveItemDetails(ItemMobileModel item)
        {
            try
            {
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                if (item.SystemId == null || item.SystemId == Guid.Empty)
                {
                    var boatId = await this.apiService.CreateItem(item).ConfigureAwait(false);
                    if (boatId != null && boatId != Guid.Empty)
                    {
                        item.SystemId = boatId;
                        item.IsSynced = true;
                    }
                    else
                    {
                        item.IsSynced = false;
                    }
                }
                else
                {
                    var boatId = await this.apiService.UpdateItem(item).ConfigureAwait(false);

                    if (boatId != null && boatId != Guid.Empty)
                    {
                        item.SystemId = boatId;
                        item.IsSynced = true;

                        UserDialogs.Instance.Toast($"Successfully uploaded {item.Description}");
                    }
                    else
                    {
                        item.IsSynced = false;
                    }
                }

                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                if (item.Id == null || item.Id == Guid.Empty)
                {
                    item.Id = await this.dataService.CreateNewItemAsync(item).ConfigureAwait(false);
                }
                else
                {
                    await this.dataService.UpdateItemAsync(item).ConfigureAwait(false);
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Saving Items Error").ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

        #region Instance Fields

        private string currentBoatId;

        private ObservableCollection<ItemMobileModel> requiredItems;

        private bool isRefreshing;

        private ItemMobileModel selectedItem;

        private IDataService dataService;

        private IServiceCommunication apiService;

        #endregion
    }
}
