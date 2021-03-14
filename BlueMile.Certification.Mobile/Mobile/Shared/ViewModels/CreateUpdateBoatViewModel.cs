using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Data;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.InternalServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    [QueryProperty(nameof(BoatId), "boatId")]
    public class CreateUpdateBoatViewModel : BaseViewModel
    {
        #region Instance Properties

        public string BoatId
        {
            get { return this.boatId; }
            set
            {
                if (this.boatId != value)
                {
                    this.boatId = Uri.UnescapeDataString(value);
                    this.GetBoat().ConfigureAwait(false);
                }
            }
        }

        public BoatMobileModel BoatDetails
        {
            get { return this.boatDetails; }
            set
            {
                if (this.boatDetails != value)
                {
                    this.boatDetails = value;
                    this.OnPropertyChanged(nameof(this.BoatDetails));
                }
            }
        }

        public bool IsJetSki
        {
            get { return this.isJetSki; }
            set
            {
                if (this.isJetSki != value)
                {
                    this.isJetSki = value;
                    this.OnPropertyChanged(nameof(this.IsJetSki));
                    BoatDetails.IsJetski = this.isJetSki;
                }
            }
        }

        public List<ListDisplayModel> BoatCategories
        {
            get { return this.boatCategories; }
            set
            {
                if (this.boatCategories != value)
                {
                    this.boatCategories = value;
                    this.OnPropertyChanged(nameof(this.BoatCategories));
                }
            }
        }

        public ListDisplayModel SelectedCategory
        {
            get { return this.selectedCategory; }
            set
            {
                if (this.selectedCategory != value)
                {
                    this.selectedCategory = value;
                    this.OnPropertyChanged(nameof(this.SelectedCategory));

                    if (this.selectedCategory.ItemId > 0)
                    {
                        this.BoatDetails.CategoryId = (BoatCategoryEnum)this.selectedCategory.ItemId;
                    }
                }
            }
        }

        public ICommand CaptureBoayancyCertPhotoCommand
        {
            get;
            private set;
        }

        public ICommand CaptureTubbiesCertPhotoCommand
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

        public CreateUpdateBoatViewModel()
        {
            this.Title = "Add New Boat";
            this.BoatDetails = new BoatMobileModel();
            this.SelectedCategory = new ListDisplayModel();
            this.BoatCategories = new List<ListDisplayModel>();
            this.BuildCategoriesList();
            this.InitCommands();

            if (!String.IsNullOrWhiteSpace(this.BoatId))
            {
                this.GetBoat().ConfigureAwait(false);
            }

            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Instance Methods

        public void InitCommands()
        {
            this.CaptureBoayancyCertPhotoCommand = new Command(async () =>
            {
                this.BoatDetails.BoyancyCertificateImage = await CapturePhotoService.CapturePhotoAsync("BoyancyCertPhoto").ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.BoatDetails));
            });
            this.CaptureTubbiesCertPhotoCommand = new Command(async () =>
            {
                this.BoatDetails.TubbiesCertificateImage = await CapturePhotoService.CapturePhotoAsync("TubbiesCertPhoto").ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.BoatDetails));
            });
            this.SaveCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Saving...");
                await this.SaveBoatDetails().ConfigureAwait(false);
            });
            this.CancelCommand = new Command(async () =>
            {
                if (await UserDialogs.Instance.ConfirmAsync("Are you sure you want to cancel capturing this boat?", "Cancel Boat?", "Yes", "No").ConfigureAwait(false))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.Navigation.PopAsync(true).ConfigureAwait(false);
                    });
                }
            });
        }

        private async Task SaveBoatDetails()
        {
            try
            {
                this.BoatDetails.OwnerId = App.OwnerId;

                if (!this.BoatDetails.IsJetski)
                {
                    this.BoatDetails.TubbiesCertificateImage.FilePath = String.Empty;
                    this.BoatDetails.TubbiesCertificateImage.ImageName = String.Empty;
                    this.BoatDetails.TubbiesCertificateImage.UniqueImageName = String.Empty;
                    this.BoatDetails.TubbiesCertificateImage.Id = Guid.Empty;
                    this.BoatDetails.TubbiesCertificateNumber = String.Empty;
                }

                if ((this.BoatDetails.BoyancyCertificateImage.Id == null) || (this.BoatDetails.BoyancyCertificateImage.Id == Guid.Empty))
                {
                    this.BoatDetails.BoyancyCertificateImage.Id = Guid.NewGuid();
                    this.BoatDetails.BoyancyCertificateImage.UniqueImageName = this.BoatDetails.BoyancyCertificateImage.Id.ToString() + ".jpg";
                }

                if (this.BoatDetails.IsJetski && (this.BoatDetails.TubbiesCertificateImage.Id == null) || (this.BoatDetails.TubbiesCertificateImage.Id == Guid.Empty))
                {
                    this.BoatDetails.TubbiesCertificateImage.Id = Guid.NewGuid();
                    this.BoatDetails.TubbiesCertificateImage.UniqueImageName = this.BoatDetails.TubbiesCertificateImage.Id.ToString() + ".jpg";
                }

                if (this.BoatDetails.BoatCategoryId <= 0)
                {
                    await UserDialogs.Instance.AlertAsync("Please select the category of the boat.", "Incomplete Boat").ConfigureAwait(false);
                }
                else if (await App.DataService.CreateNewBoatAsync(BoatDetails).ConfigureAwait(false))
                {
                    UserDialogs.Instance.Toast($"Successfulle saved {this.BoatDetails.Name}");

                    BoatMobileModel boat = new BoatMobileModel();
                    if ((await App.ApiService.GetBoatById(BoatDetails.Id).ConfigureAwait(false)) == null)
                    {
                        boat = await App.ApiService.CreateBoat(this.BoatDetails).ConfigureAwait(false);
                    }
                    else
                    {
                        boat = await App.ApiService.UpdateBoat(this.BoatDetails).ConfigureAwait(false);
                    }


                    if (boat != null)
                    {
                        var syncResult = await App.DataService.UpdateBoatAsync(this.BoatDetails).ConfigureAwait(false);
                        UserDialogs.Instance.Toast($"Successfully uploaded {this.BoatDetails.Name}");
                    }

                    UserDialogs.Instance.HideLoading();
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.Navigation.PopAsync().ConfigureAwait(false);
                    });
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Could not successfully save this boat. Please try again.", "Unsuccessfully Saved", "Ok").ConfigureAwait(false);
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Saving Boat Error").ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            }
        }

        private void BuildCategoriesList()
        {
            this.BoatCategories.Clear();
            var categories = Enum.GetValues(typeof(BoatCategoryEnum)).Cast<BoatCategoryEnum>().ToList();
            foreach (var category in categories)
            {
                this.BoatCategories.Add(new ListDisplayModel
                {
                    ItemId = (int)category,
                    ItemName = GetCategoryDescription(category)
                });
            }
        }

        public static string GetCategoryDescription(BoatCategoryEnum enumId)
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

        private async Task GetBoat()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(this.BoatId))
                {
                    this.BoatDetails = await App.DataService.FindBoatBySystemIdAsync(Guid.Parse(this.BoatId)).ConfigureAwait(false);

                    if (this.BoatDetails != null)
                    {
                        this.Title = $"Edit {this.BoatDetails.Name}";
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Boat Error").ConfigureAwait(false);
            }
        }

        #endregion

        #region Instance Fields

        private BoatMobileModel boatDetails;

        private string boatId;

        private List<ListDisplayModel> boatCategories;

        private ListDisplayModel selectedCategory;

        private bool isJetSki;

        #endregion
    }
}
