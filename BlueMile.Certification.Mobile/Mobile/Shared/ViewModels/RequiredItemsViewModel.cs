using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services.InternalServices;
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
        }

        private async Task CreateNewItem()
        {
            try
            {
                var destinationRoute = "items/new";
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{destinationRoute}?boatId={this.CurrentBoatId}").ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "New Item Error").ConfigureAwait(false);
            }
        }

        private async Task GetBoatItems()
        {
            this.RequiredItems = new ObservableCollection<ItemMobileModel>(await App.DataService.FindItemsByBoatIdAsync(Guid.Parse(this.CurrentBoatId)).ConfigureAwait(false));
        }

        private async void OpenItemDetail()
        {
            UserDialogs.Instance.ShowLoading("Loading...");
            var destinationRoute = $"items/detail";
            await Shell.Current.GoToAsync($"{destinationRoute}?itemId={this.SelectedItem.Id}").ConfigureAwait(false);
            Shell.Current.FlyoutIsPresented = false;

            //UserDialogs.Instance.ShowLoading("Loading...");
            //var destinationRoute = "items/detail";
            //ShellNavigationState state = Shell.Current.CurrentState;
            //await Shell.Current.GoToAsync($"{destinationRoute}?itemId={this.SelectedItem.Id.ToString()}").ConfigureAwait(false);
            //Shell.Current.FlyoutIsPresented = false;
        }

        #endregion

        #region Instance Fields

        private string currentBoatId;

        private ObservableCollection<ItemMobileModel> requiredItems;

        private bool isRefreshing;

        private ItemMobileModel selectedItem;

        #endregion
    }
}
