using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Converters;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Globalization;
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

        public ICommand CloseCommand
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
            this.CloseCommand = new Command(async () =>
            {
                await Shell.Current.Navigation.PopAsync().ConfigureAwait(false);
            });
            this.EditItemCommand = new Command(async () =>
            {
            });
        }

        private async Task GetItemDetail()
        {
            try
            {
                this.SelectedItem = await App.DataService.FindItemBySystemIdAsync(Guid.Parse(this.CurrentItemId)).ConfigureAwait(false);
                this.Title = ItemTypeDescriptionConverter.GetDescription((ItemTypeEnum)this.SelectedItem.ItemTypeId);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Item Error").ConfigureAwait(false);
            }
        }

        #endregion

        #region Instance Fields

        private ItemMobileModel selectedItem;

        private string currentItemId;

        #endregion
    }
}
