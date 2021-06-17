using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Converters;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using Microsoft.AppCenter.Crashes;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    [QueryProperty(nameof(CurrentItemId), "itemId")]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Instance Properties

        public string CurrentItemId
        {
            get { return this.currentItemId; }
            set
            {
                this.currentItemId = Uri.UnescapeDataString(value);
                this.GetItemDetail().ConfigureAwait(false);
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
                }
            }
        }

        public ICommand EditItemCommand
        {
            get;
            private set;
        }

        public ICommand SyncCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public ItemDetailViewModel()
        {
            this.SelectedItem = new ItemMobileModel();
            if (!String.IsNullOrWhiteSpace(this.CurrentItemId))
            {
                this.GetItemDetail().ConfigureAwait(false);
            }
            this.InitCommands();

            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.EditItemCommand = new Command(async () =>
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.itemEditRoute}?itemId={this.SelectedItem.Id}", true);
            });
            this.SyncCommand = new Command(async () =>
            {
                await this.UploadItemToServer();
            });
        }

        private async Task UploadItemToServer()
        {
            try
            {
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                if (this.SelectedItem.Id == null || this.SelectedItem.Id == Guid.Empty)
                {
                    Guid itemId;
                    try
                    {
                        itemId = await this.apiService.CreateItem(this.SelectedItem);
                    }
                    catch (WebException)
                    {
                        itemId = Guid.Empty;
                    }

                    if (itemId != null && itemId != Guid.Empty)
                    {
                        this.SelectedItem.Id = itemId;
                        this.SelectedItem.IsSynced = true;
                    }
                    else
                    {
                        this.SelectedItem.IsSynced = false;
                    }
                }
                else
                {
                    Guid itemId;
                    try
                    {
                        itemId = await this.apiService.UpdateItem(this.SelectedItem);
                    }
                    catch (WebException)
                    {
                        itemId = Guid.Empty;
                    }

                    if (itemId != null && itemId != Guid.Empty)
                    {
                        this.SelectedItem.Id = itemId;
                        this.SelectedItem.IsSynced = true;
                    }
                    else
                    {
                        this.SelectedItem.IsSynced = false;
                    }
                }

                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var syncResult = await this.dataService.UpdateItemAsync(this.SelectedItem);
                UserDialogs.Instance.Toast($"Successfully uploaded {this.SelectedItem.Description}");
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Sync Error");
            }
        }

        private async Task GetItemDetail()
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                this.SelectedItem = await this.dataService.FindItemBySystemIdAsync(Guid.Parse(this.CurrentItemId));
                this.Title = ItemTypeDescriptionConverter.GetDescription((ItemTypeEnum)this.SelectedItem.ItemTypeId);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Item Error");
            }
        }

        #endregion

        #region Instance Fields

        private ItemMobileModel selectedItem;

        private string currentItemId;

        private IDataService dataService;

        private IServiceCommunication apiService;

        #endregion
    }
}
