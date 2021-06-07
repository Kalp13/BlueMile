using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Helpers;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Web.ApiModels.Helper;
using Microsoft.AppCenter.Crashes;
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
            if (String.IsNullOrWhiteSpace(SettingsService.OwnerId))
            {
                this.OwnerDetails = new OwnerMobileModel();
                base.Title = "Capture Owner Details";
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
        }

        private async Task GetOwnerDetails()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(SettingsService.OwnerId))
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    var ownerId = Guid.Parse(SettingsService.OwnerId);
                    this.OwnerDetails = await this.dataService.FindOwnerBySystemIdAsync(ownerId).ConfigureAwait(false);

                    if (this.OwnerDetails != null)
                    {
                        this.Title = "Edit " + this.OwnerDetails.FirstName;
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
                if (await UserDialogs.Instance.ConfirmAsync($"Are the following details correct:\n{this.OwnerDetails.ToString()}").ConfigureAwait(false))
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    if (this.OwnerDetails.IcasaPopPhoto != null)
                    {
                        if (this.OwnerDetails.IcasaPopPhoto?.Id == null || this.OwnerDetails.IcasaPopPhoto?.Id == Guid.Empty)
                        {
                            this.OwnerDetails.IcasaPopPhoto.Id = Guid.NewGuid();
                            this.OwnerDetails.IcasaPopPhoto.UniqueImageName = this.OwnerDetails.IcasaPopPhoto.Id.ToString() + ".jpg";
                        }
                    }

                    if (this.OwnerDetails.IdentificationDocument != null)
                    {
                        if (this.OwnerDetails.IdentificationDocument?.Id == null || this.OwnerDetails.IdentificationDocument?.Id == Guid.Empty)
                        {
                            this.OwnerDetails.IdentificationDocument.Id = Guid.NewGuid();
                            this.OwnerDetails.IdentificationDocument.UniqueImageName = this.OwnerDetails.IdentificationDocument.Id.ToString() + ".jpg";
                        }
                    }

                    if (this.OwnerDetails.SkippersLicenseImage != null)
                    {
                        if (this.OwnerDetails.SkippersLicenseImage?.Id == null || this.OwnerDetails.SkippersLicenseImage?.Id == Guid.Empty)
                        {
                            this.OwnerDetails.SkippersLicenseImage.Id = Guid.NewGuid();
                            this.OwnerDetails.SkippersLicenseImage.UniqueImageName = this.OwnerDetails.SkippersLicenseImage.Id.ToString() + ".jpg";
                        }
                    }

                    if (this.OwnerDetails.Id == null || this.OwnerDetails.Id == Guid.Empty)
                    {
                        this.OwnerDetails.Id = await this.dataService.CreateNewOwnerAsync(this.OwnerDetails).ConfigureAwait(false); 
                        if (this.OwnerDetails.SystemId == null || this.OwnerDetails.SystemId == Guid.Empty)
                        {
                            if (this.apiService == null)
                            {
                                this.apiService = new ServiceCommunication();
                            }

                            UserDialogs.Instance.Toast("Successfully created Owner: " + this.OwnerDetails.FirstName);

                            var ownerId = await this.apiService.CreateOwner(this.OwnerDetails).ConfigureAwait(false);
                            if (ownerId != null && ownerId != Guid.Empty)
                            {
                                SettingsService.OwnerId = ownerId.ToString();
                                this.OwnerDetails.IsSynced = true;
                                UserDialogs.Instance.Toast("Successfully created owner details on server.");
                            }
                            else
                            {
                                this.OwnerDetails.IsSynced = false;
                                await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.").ConfigureAwait(false);
                            }

                            var syncResult = await this.dataService.UpdateOwnerAsync(this.OwnerDetails).ConfigureAwait(false);

                            MessagingCenter.Instance.Send<OwnerMobileModel, string>(this.OwnerDetails, "Owner", "");

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Shell.Current.Navigation.PopAsync().ConfigureAwait(false);
                            });
                        }
                    }
                    else
                    {
                        var result = await this.dataService.UpdateOwnerAsync(this.OwnerDetails).ConfigureAwait(false);
                        if (result)
                        {
                            if (this.apiService == null)
                            {
                                this.apiService = new ServiceCommunication();
                            }

                            UserDialogs.Instance.Toast("Successfully updated Owner: " + this.OwnerDetails.FirstName);
                            var ownerId = await this.apiService.UpdateOwner(this.OwnerDetails).ConfigureAwait(false);
                            if (ownerId != null && ownerId != Guid.Empty)
                            {
                                SettingsService.OwnerId = ownerId.ToString();
                                this.OwnerDetails.IsSynced = true;
                                UserDialogs.Instance.Toast("Successfully updated owner details to server.");
                            }
                            else
                            {
                                this.OwnerDetails.IsSynced = false;
                                await UserDialogs.Instance.AlertAsync("Could not update your details on the server. Please try again later.").ConfigureAwait(false);
                            }

                            var syncResult = await this.dataService.UpdateOwnerAsync(this.OwnerDetails).ConfigureAwait(false);

                            MessagingCenter.Instance.Send<OwnerMobileModel, string>(this.OwnerDetails, "Owner", "");
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Shell.Current.Navigation.PopAsync(true).ConfigureAwait(false);
                            });
                        }
                    }
                }
            }
            catch (WebException webExc)
            {
                await UserDialogs.Instance.AlertAsync($"{webExc.Status} - {webExc.Message} - {webExc.Response}", "Save Owner Error").ConfigureAwait(false);
                Crashes.TrackError(webExc);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message + " - " + exc.InnerException?.Message, "Save Owner Error").ConfigureAwait(false);
                Crashes.TrackError(exc);
            }
        }

        #endregion

        #region Instance Fields

        private OwnerMobileModel ownerDetails;

        private IDataService dataService;

        private IServiceCommunication apiService;

        #endregion
    }
}
