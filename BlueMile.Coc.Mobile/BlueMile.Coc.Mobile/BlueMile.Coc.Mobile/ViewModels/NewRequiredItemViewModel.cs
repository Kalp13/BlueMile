using Acr.UserDialogs;
using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using BlueMile.Coc.Mobile.Services;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.ViewModels
{
    [QueryProperty("CurrentBoatId", "boatId")]
    public class NewRequiredItemViewModel : BaseViewModel
    {
        #region Instance Properties

        public string CurrentBoatId
        {
            get { return this.currentBoatId; }
            set
            {
                this.currentBoatId = Uri.UnescapeDataString(value);

                if (this.NewItem != null)
                {
                    this.NewItem.BoatId = Guid.Parse(this.currentBoatId);
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

                    if (this.selectedItemType.ItemId != null)
                    {
                        this.NewItem.ItemTypeId = (ItemTypeStaticEntity)this.selectedItemType.ItemId;
                    }
                }
            }
        }

        public RequiredItemModel NewItem
        {
            get { return this.newItem; }
            set
            {
                if (this.newItem != value)
                {
                    this.newItem = value;
                    this.OnPropertyChanged(nameof(this.NewItem));
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

        public NewRequiredItemViewModel()
        {
            this.NewItem = new RequiredItemModel();
            this.Title = "New Item For ";
            this.ItemTypes = new List<ListDisplayModel>();
            this.SelectedItemType = new ListDisplayModel();
            this.BuildItemTypeList();
            this.InitCommands();
        }

        #endregion

        #region Instance Methods
        
        private void InitCommands()
        {
            this.CaptureItemPhoto = new Command(async () =>
            {
                this.NewItem.ItemImage = await CapturePhotoService.CapturePhotoAsync(this.NewItem.ItemTypeId.ToString()).ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.NewItem));
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

        private async Task SaveItemDetails()
        {
            try
            {
                this.NewItem.CapturedDate = DateTime.Now;
                this.NewItem.BoatId = Guid.Parse(this.CurrentBoatId);

                if (this.NewItem.Id == Guid.Empty || this.NewItem.Id == null)
                {
                    this.NewItem.Id = Guid.NewGuid();
                }

                if (this.NewItem.ItemImage.Id == null || this.NewItem.ItemImage.Id == Guid.Empty)
                {
                    this.NewItem.ItemImage.Id = Guid.NewGuid();
                    this.NewItem.ItemImage.UniqueImageName = this.NewItem.ItemImage.Id.ToString() + ".jpg";
                }

                if (await this.ValidateItemPropertiesAsync().ConfigureAwait(false))
                {
                    if (await App.DataService.CreateNewImage(this.NewItem.ItemImage).ConfigureAwait(false))
                    {
                        if (await App.DataService.CreateNewItem(this.NewItem).ConfigureAwait(false))
                        {
                            if (await App.ApiService.CreateImage(this.NewItem.ItemImage).ConfigureAwait(false))
                            {
                                if ((await App.ApiService.CreateItem(this.NewItem).ConfigureAwait(false)) != null)
                                {
                                    UserDialogs.Instance.Toast("Successfully created " + this.NewItem.ItemTypeId, TimeSpan.FromSeconds(2));

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Shell.Current.Navigation.PopAsync(true).ConfigureAwait(false);
                                        UserDialogs.Instance.HideLoading();
                                    });
                                }
                                else
                                {
                                    await UserDialogs.Instance.AlertAsync("Could not upload item data.").ConfigureAwait(false);
                                }
                            }
                            else
                            {
                                await UserDialogs.Instance.AlertAsync("Could not upload item data.").ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync("Item was not saved successfully. Please try again.", "Save Unsuccessfull").ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Could not save item image").ConfigureAwait(false);
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
            if (this.NewItem.ItemTypeId <= 0)
            {
                await UserDialogs.Instance.AlertAsync("Please select the type of item.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }

            if (this.NewItem.BoatId == Guid.Empty || this.NewItem.BoatId == null)
            {
                await UserDialogs.Instance.AlertAsync("Invalid boat linked. Please ensure that you are adding this item to valid boat.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }

            if (String.IsNullOrWhiteSpace(this.NewItem.ItemImage.FileName) || 
                String.IsNullOrWhiteSpace(this.NewItem.ItemImage.FilePath) || 
                (this.NewItem.ItemImage.Id == Guid.Empty) || 
                (this.NewItem.ItemImage.Id == null))
            {
                await UserDialogs.Instance.AlertAsync("No image has been captured for the item. Please capture an image before continuing.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }
            if (await CanItemExpire(this.NewItem.ItemTypeId).ConfigureAwait(false) && DateTime.Compare(DateTime.Today.AddMonths(6), this.NewItem.ExpiryDate) >= 0)
            {
                await UserDialogs.Instance.AlertAsync("You cannot add an item that expires within 6 months.", "Incomplete Item").ConfigureAwait(false);
                return false;
            }

            return true;
        }

        private static Task<bool> CanItemExpire(ItemTypeStaticEntity itemType)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            switch (itemType)
            {
                case ItemTypeStaticEntity.Anchor:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.AnchorRope:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.CapsizeBottleWith2mLaneyard:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.CodeFlag:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.DrogueAnchor:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.FireExtinguisher:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeStaticEntity.FirstAidKit:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.FittedGrabline:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.FogHorn:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.HandHeldFlare:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeStaticEntity.HandheldSpotlight:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.IdSheet:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.LifeJacket:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.MagneticCompass:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.OarOrPaddle:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.ParachuteFlare:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeStaticEntity.RadarReflector:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.SmokeFlare:
                    completionSource.SetResult(true);
                    break;
                case ItemTypeStaticEntity.SpaceBlanket:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.TowRope:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.VhfRadio:
                    completionSource.SetResult(false);
                    break;
                case ItemTypeStaticEntity.WaterproofTorch:
                    completionSource.SetResult(false);
                    break;
            }

            return completionSource.Task;
        }

        public void BuildItemTypeList()
        {
            this.ItemTypes.Clear();
            var types = Enum.GetValues(typeof(ItemTypeStaticEntity)).Cast<ItemTypeStaticEntity>().ToList();
            foreach (var type in types)
            {
                this.ItemTypes.Add(new ListDisplayModel
                {
                    ItemId = type,
                    ItemName = GetItemTypeDescription(type)
                });
            }
        }

        public static string GetItemTypeDescription(ItemTypeStaticEntity x)
        {
            Type type = x.GetType();
            MemberInfo[] memInfo = type.GetMember(x.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return x.ToString();
        }

        #endregion

        #region Instance Fields

        private string currentBoatId;

        private RequiredItemModel newItem;

        private List<ListDisplayModel> itemTypes;

        private ListDisplayModel selectedItemType;

        #endregion
    }
}
