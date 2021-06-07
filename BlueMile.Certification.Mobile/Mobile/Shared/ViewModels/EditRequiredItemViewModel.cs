using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Data;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    [QueryProperty(nameof(RequiredItemId), "itemId")]
    public class EditRequiredItemViewModel : BaseViewModel
    {
        #region Instance Properties

        public string RequiredItemId
        {
            get { return this.requiredItemId; }
            set
            {
                this.requiredItemId = Uri.UnescapeDataString(value);
                this.GetItem().ConfigureAwait(false);
            }
        }

        public ItemMobileModel ItemToUpdate
        {
            get { return this.itemToUpdate; }
            set
            {
                if (this.itemToUpdate != value)
                {
                    this.itemToUpdate = value;
                    this.OnPropertyChanged(nameof(this.ItemToUpdate));
                }
            }
        }

        public List<ListDisplayModel> ItemTypes
        {
            get { return this.itemTypes; }
            set
            {
                if (this.itemTypes != value)
                {
                    this.itemTypes = value;
                    this.OnPropertyChanged(nameof(this.ItemTypes));
                }
            }
        }

        public ListDisplayModel SelectedItemType
        {
            get { return this.selectedItemType; }
            set
            {
                if (this.selectedItemType != value)
                {
                    this.selectedItemType = value;
                    this.OnPropertyChanged(nameof(this.SelectedItemType));

                    if (this.selectedItemType.ItemId > 0)
                    {
                        this.ItemToUpdate.ItemTypeId = this.selectedItemType.ItemId;
                    }
                }
            }
        }

        public ICommand CaptureItemPhoto
        {
            get;
            private set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public ICommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public EditRequiredItemViewModel()
        {

            this.ItemToUpdate = new ItemMobileModel();
            this.ItemTypes = new List<ListDisplayModel>();
            this.SelectedItemType = new ListDisplayModel();
            this.BuildItemTypeList();

            this.InitCommands();

            if (!String.IsNullOrWhiteSpace(this.RequiredItemId))
            {
                this.GetItem().ConfigureAwait(false);
            }

            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.CaptureItemPhoto = new Command(async () =>
            {
                this.ItemToUpdate.ItemImage = await CapturePhotoService.CapturePhotoAsync(this.ItemToUpdate.ItemTypeId.ToString()).ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.ItemToUpdate));
            });
            this.SaveCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Saving...");
                await this.SaveItemDetails().ConfigureAwait(false);
            });
            this.CancelCommand = new Command(async () =>
            {
                if (await UserDialogs.Instance.ConfirmAsync("Are you sure you want to cancel creating this item?", "Cancel Item", "Yes", "No").ConfigureAwait(false))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.Navigation.PopAsync(true).ConfigureAwait(false);
                        UserDialogs.Instance.HideLoading();
                    });
                }
            });
        }

        private async Task GetItem()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(this.RequiredItemId))
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    this.ItemToUpdate = await this.dataService.FindItemBySystemIdAsync(Guid.Parse(this.RequiredItemId)).ConfigureAwait(false);

                    if (this.ItemToUpdate != null)
                    {
                        this.Title = $"Edit {this.ItemToUpdate.Description}";
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Boat Error").ConfigureAwait(false);
            }
        }

        private async Task SaveItemDetails()
        {
            try
            {
                this.ItemToUpdate.CapturedDate = DateTime.Now;

                if (this.ItemToUpdate.ItemImage.Id != null && this.ItemToUpdate.ItemImage.Id != Guid.Empty)
                {
                    this.ItemToUpdate.ItemImage.UniqueImageName = this.ItemToUpdate.ItemImage.Id.ToString() + ".jpg";
                }

                if (await this.ValidateItemPropertiesAsync().ConfigureAwait(false))
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    if (await this.dataService.UpdateItemAsync(this.ItemToUpdate).ConfigureAwait(false))
                    {
                        if (this.apiService == null)
                        {
                            this.apiService = new ServiceCommunication();
                        }

                        if (this.ItemToUpdate.SystemId == null || this.ItemToUpdate.SystemId == Guid.Empty)
                        {
                            var itemId = await this.apiService.CreateItem(this.ItemToUpdate).ConfigureAwait(false);

                            if (itemId != null && itemId != Guid.Empty)
                            {
                                this.ItemToUpdate.IsSynced = true;
                                this.ItemToUpdate.SystemId = itemId;
                                UserDialogs.Instance.Toast("Successfully uploaded " + this.ItemToUpdate.Description);
                            }
                            else
                            {
                                this.ItemToUpdate.IsSynced = false;
                            }
                        }
                        else
                        {
                            var itemId = await this.apiService.UpdateItem(this.ItemToUpdate).ConfigureAwait(false);

                            if (itemId != null && itemId != Guid.Empty)
                            {
                                this.ItemToUpdate.IsSynced = true;
                                this.ItemToUpdate.SystemId = itemId;
                                UserDialogs.Instance.Toast("Successfully updated " + this.ItemToUpdate.Description);
                            }
                            else
                            {
                                this.ItemToUpdate.IsSynced = false;
                            }
                        }

                        var syncResult = await this.dataService.UpdateItemAsync(this.ItemToUpdate).ConfigureAwait(false);

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Shell.Current.Navigation.PopAsync(true).ConfigureAwait(false);
                            UserDialogs.Instance.HideLoading();
                        });
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Item was not saved successfully. Please try again.", "Save Unsuccessfull").ConfigureAwait(false);
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Saving Item Error").ConfigureAwait(false);
            }
        }

        private async Task<bool> ValidateItemPropertiesAsync()
        {
            if (this.ItemToUpdate.ItemTypeId <= 0)
            {
                await UserDialogs.Instance.AlertAsync("Please select the type of item.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }

            if (this.ItemToUpdate.BoatId == Guid.Empty || this.ItemToUpdate.BoatId == null)
            {
                await UserDialogs.Instance.AlertAsync("Invalid boat linked. Please ensure that you are adding this item to valid boat.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }

            if (String.IsNullOrWhiteSpace(this.ItemToUpdate.ItemImage.FileName) ||
                String.IsNullOrWhiteSpace(this.ItemToUpdate.ItemImage.FilePath) ||
                (this.ItemToUpdate.ItemImage.Id != null && 
                this.ItemToUpdate.ItemImage.Id != Guid.Empty))
            {
                await UserDialogs.Instance.AlertAsync("No image has been captured for the item. Please capture an image before continuing.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }
            if (await CanItemExpire((ItemTypeEnum)this.ItemToUpdate.ItemTypeId).ConfigureAwait(false) && 
                DateTime.Compare(DateTime.Today.AddMonths(6), this.ItemToUpdate.ExpiryDate) >= 0)
            {
                await UserDialogs.Instance.AlertAsync("You cannot add an item that expires within 6 months.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }

            return true;
        }

        private static Task<bool> CanItemExpire(ItemTypeEnum itemType)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            switch (itemType)
            {
                case ItemTypeEnum.Anchor:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.AnchorRope:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.CapsizeBottleWith2mLaneyard:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.CodeFlag:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.DrogueAnchor:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.FireExtinguisher:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeEnum.FirstAidKit:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.FittedGrabline:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.FogHorn:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.HandHeldFlare:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeEnum.HandheldSpotlight:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.IdSheet:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.LifeJacket:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.MagneticCompass:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.OarOrPaddle:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.ParachuteFlare:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeEnum.RadarReflector:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.SmokeFlare:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeEnum.SpaceBlanket:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.TowRope:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.VhfRadio:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeEnum.WaterproofTorch:
                    completionSource.SetResult(false);
                    break;
            }

            return completionSource.Task;
        }

        public void BuildItemTypeList()
        {
            this.ItemTypes.Clear();
            var types = Enum.GetValues(typeof(ItemTypeEnum)).Cast<ItemTypeEnum>().ToList();
            foreach (var type in types)
            {
                this.ItemTypes.Add(new ListDisplayModel
                {
                    ItemId = (int)type,
                    ItemName = GetItemTypeDescription(type)
                });
            }
        }

        public static string GetItemTypeDescription(ItemTypeEnum enumId)
        {
            Type type = enumId.GetType();
            MemberInfo[] memInfo = type.GetMember(enumId.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumId.ToString();
        }

        #endregion

        #region Instance Fields

        private string requiredItemId;

        private ItemMobileModel itemToUpdate;

        private List<ListDisplayModel> itemTypes;

        private ListDisplayModel selectedItemType;

        private IServiceCommunication apiService;

        private IDataService dataService;

        #endregion
    }
}
