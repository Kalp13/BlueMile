using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.InternalServices;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class CreateUpdateOwnerViewModel : BaseViewModel
    {
        #region Instance Properties

        public OwnerMobileModel OwnerDetails
        {
            get { return this.ownerDetails; }
            set
            {
                if (this.ownerDetails != value)
                {
                    this.ownerDetails = value;
                    base.OnPropertyChanged(nameof(this.OwnerDetails));
                }
            }
        }

        public ICommand CancelCommand
        {
            get;
            private set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public ICommand CaptureIDPhotoCommand
        {
            get;
            private set;
        }

        public ICommand CaptureSkippersPhotoCommand
        {
            get;
            private set;
        }

        public ICommand CapturePassportPhotoCommand
        {
            get;
            private set;
        }

        public ICommand CaptureIcasaPopPhotoCommand
        {
            get;
            private set;
        }

        public ICommand GetAddressCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public CreateUpdateOwnerViewModel()
        {
            base.Title = "Capture Owner Details";
            if (String.IsNullOrWhiteSpace(SettingsService.OwnerId))
            {
                this.OwnerDetails = new OwnerMobileModel();
            }
            else
            {
                this.GetOwnerDetails().ConfigureAwait(false);
            }
            this.InitCommands();
        }

        #endregion

        #region Class Methods

        private void InitCommands()
        {
            this.SaveCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Saving...");
                await this.SaveOwnerDetails().ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            });
            this.CancelCommand = new Command(async () =>
            {
                await Shell.Current.Navigation.PopAsync().ConfigureAwait(false);
            });
            this.CaptureIcasaPopPhotoCommand = new Command(async () =>
            {
                this.OwnerDetails.IcasaPopPhoto = await CapturePhotoService.CapturePhotoAsync("IcasaPopPhoto").ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.OwnerDetails));
            });
            this.CaptureIDPhotoCommand = new Command(async () =>
            {
                this.OwnerDetails.IdentificationDocument = await CapturePhotoService.CapturePhotoAsync("IDPhoto").ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.OwnerDetails));
            });
            this.CaptureSkippersPhotoCommand = new Command(async () =>
            {
                this.OwnerDetails.SkippersLicenseImage = await CapturePhotoService.CapturePhotoAsync("SkippersPhoto").ConfigureAwait(false);
                this.OnPropertyChanged(nameof(this.OwnerDetails));
            });
            this.GetAddressCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Retrieving Address");
                await this.GetLocationAndAddress().ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            });
        }

        private async Task GetLocationAndAddress()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request).ConfigureAwait(false);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    this.OwnerDetails.AddressLine1 = String.Format(CultureInfo.InvariantCulture,
                                                    "Lat:{0}; Long:{1}",
                                                    location.Latitude,
                                                    location.Longitude);
                    this.OnPropertyChanged(nameof(OwnerDetails));
                    //var placeMarks = await Geocoding.GetPlacemarksAsync(location).ConfigureAwait(false);
                    //var placeMark = placeMarks?.FirstOrDefault();

                    //if (placeMark != null)
                    //{
                    //    this.NewOwner.Address = placeMark.ToString();
                    //}
                }
            }
            catch (FeatureNotSupportedException fnsExc)
            {
                await UserDialogs.Instance.AlertAsync(fnsExc.Message + " - " + fnsExc.InnerException?.Message, "Create Owner Error").ConfigureAwait(false);
            }
            catch (FeatureNotEnabledException fneExc)
            {
                await UserDialogs.Instance.AlertAsync(fneExc.Message + " - " + fneExc.InnerException?.Message, "Create Owner Error").ConfigureAwait(false);
            }
            catch (PermissionException pExc)
            {
                await UserDialogs.Instance.AlertAsync(pExc.Message + " - " + pExc.InnerException?.Message, "Create Owner Error").ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message + " - " + exc.InnerException?.Message, "Create Owner Error").ConfigureAwait(false);
            }
        }

        private async Task GetOwnerDetails()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(SettingsService.OwnerId))
                {
                    this.OwnerDetails = await App.DataService.FindOwnerBySystemIdAsync(Guid.Parse(SettingsService.OwnerId)).ConfigureAwait(false);

                    if (this.OwnerDetails != null)
                    {
                        this.Title = "Edit " + this.OwnerDetails.Name;
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, " GetOwner Error").ConfigureAwait(false);
            }
        }

        private async Task SaveOwnerDetails()
        {
            try
            {
                if (await UserDialogs.Instance.ConfirmAsync(this.OwnerDetails.ToString()).ConfigureAwait(false))
                {
                    if (this.OwnerDetails.IcasaPopPhoto.Id <= 0)
                    {
                        this.OwnerDetails.IcasaPopPhoto.UniqueImageName = this.OwnerDetails.IcasaPopPhoto.Id.ToString() + ".jpg";
                    }

                    if (this.OwnerDetails.IdentificationDocument.Id <= 0)
                    {
                        this.OwnerDetails.IdentificationDocument.UniqueImageName = this.OwnerDetails.IdentificationDocument.Id.ToString() + ".jpg";
                    }

                    if (this.OwnerDetails.SkippersLicenseImage.Id <= 0)
                    {
                        this.OwnerDetails.SkippersLicenseImage.UniqueImageName = this.OwnerDetails.SkippersLicenseImage.Id.ToString() + ".jpg";
                    }

                    var result = await App.DataService.CreateNewOwnerAsync(this.OwnerDetails).ConfigureAwait(false);

                    if (result)
                    {
                        UserDialogs.Instance.Toast("Successfully created Owner: " + this.OwnerDetails.Name, TimeSpan.FromSeconds(2));
                        SettingsService.OwnerId = this.OwnerDetails.Id.ToString();
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Shell.Current.Navigation.PopAsync().ConfigureAwait(false);
                        });
                    }

                    var owner = await App.ApiService.CreateOwner(this.OwnerDetails).ConfigureAwait(false);
                    if (owner != null)
                    {
                        this.OwnerDetails.IsSynced = true;
                        var syncResult = await App.DataService.UpdateOwnerAsync(this.OwnerDetails).ConfigureAwait(false);
                        UserDialogs.Instance.Toast("Successfully saved owner details to server.", TimeSpan.FromSeconds(5));
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.", "Create Error").ConfigureAwait(false);
                    }
                }
            }
            catch (WebException webExc)
            {
                await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0} - {1} - {2}", webExc.Status.ToString(), webExc.Message, webExc.Response)).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message + " - " + exc.InnerException?.Message, "Create Owner Error").ConfigureAwait(false);
            }
        }

        #endregion

        #region Instance Fields

        private OwnerMobileModel ownerDetails;

        #endregion
    }
}
