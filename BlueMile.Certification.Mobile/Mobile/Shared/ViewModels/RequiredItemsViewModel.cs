using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
                await this.SyncItemsWithServer();
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

        private async Task SyncItemsWithServer()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Syncing...");

                foreach (var item in this.RequiredItems.Where(x => !x.IsSynced))
                {
                    await this.SaveItemDetails(item);
                }

                await this.GetItemsFromServer();

                await this.GetBoatItems();
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Sync Error");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task GetItemsFromServer()
        {
            try
            {
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                var items = await this.apiService.GetBoatRequiredItems(Guid.Parse(this.CurrentBoatId));

                foreach (var item in items)
                {
                    await this.SaveItemDetailsLocally(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SaveItemDetailsLocally(ItemMobileModel item)
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var exists = await this.dataService.FindItemByIdAsync(item.Id);
                item.IsSynced = true;

                if (exists == null)
                {
                    if (item.ItemImage != null)
                    {
                        item.ItemImage.FilePath = Path.Combine(FileSystem.CacheDirectory, item.ItemImage.UniqueFileName);
                        await File.WriteAllBytesAsync(item.ItemImage.FilePath, item.ItemImage.FileContent);
                    }
                    item.Id = await this.dataService.CreateNewItemAsync(item);
                }
                else if (await UserDialogs.Instance.ConfirmAsync($"Would you like to replace\n{exists.ToString()}with\n{item.ToString()}"))
                {
                    if (item.ItemImage != null)
                    {
                        item.ItemImage.FilePath = Path.Combine(FileSystem.CacheDirectory, item.ItemImage.UniqueFileName);
                        await File.WriteAllBytesAsync(item.ItemImage.FilePath, item.ItemImage.FileContent);
                    }
                    await this.dataService.UpdateItemAsync(item);
                }
            }
            catch (Exception)
            {
                throw;
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

                var doesExist = await this.apiService.DoesItemExist(item.Id);

                if (!doesExist)
                {
                    var itemId = await this.apiService.CreateItem(item).ConfigureAwait(false);
                    if (itemId != null && itemId != Guid.Empty)
                    {
                        item.Id = itemId;
                        item.IsSynced = true;
                    }
                    else
                    {
                        item.IsSynced = false;
                    }
                }
                else
                {
                    var itemId = await this.apiService.UpdateItem(item).ConfigureAwait(false);

                    if (itemId != null && itemId != Guid.Empty)
                    {
                        item.Id = itemId;
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
                    item.Id = await this.dataService.CreateNewItemAsync(item);
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
