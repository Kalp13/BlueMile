using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
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
                await this.SaveOwnerDetails();
                UserDialogs.Instance.HideLoading();
            });
            this.CancelCommand = new Command(async () =>
            {
                await Shell.Current.Navigation.PopAsync();
            });
            this.CaptureIcasaPopPhotoCommand = new Command(async () =>
            {
                var image = await CapturePhotoService.CapturePhotoAsync("IcasaPopPhoto");
                this.OwnerDetails.IcasaPopPhoto = new OwnerDocumentMobileModel()
                {
                    DocumentTypeId = (int)DocumentTypeEnum.IcasaProofOfPayment,
                    FileName = image.FileName,
                    FilePath = image.FilePath,
                    Id = image.Id,
                    MimeType = image.FileType,
                    UniqueFileName = image.Id.ToString() + ".jpg"
                };
                this.OnPropertyChanged(nameof(this.OwnerDetails));
            });
            this.CaptureIDPhotoCommand = new Command(async () =>
            {
                var image = await CapturePhotoService.CapturePhotoAsync("IDPhoto");
                this.OwnerDetails.IdentificationDocument = new OwnerDocumentMobileModel()
                {
                    DocumentTypeId = (int)DocumentTypeEnum.IdentificationDocument,
                    FileName = image.FileName,
                    FilePath = image.FilePath,
                    Id = image.Id,
                    MimeType = image.FileType,
                    UniqueFileName = image.Id.ToString() + ".jpg"
                };
                this.OnPropertyChanged(nameof(this.OwnerDetails));
            });
            this.CaptureSkippersPhotoCommand = new Command(async () =>
            {
                var image = await CapturePhotoService.CapturePhotoAsync("SkippersPhoto");
                this.OwnerDetails.SkippersLicenseImage = new OwnerDocumentMobileModel()
                {
                    DocumentTypeId = (int)DocumentTypeEnum.SkippersLicense,
                    FileName = image.FileName,
                    FilePath = image.FilePath,
                    Id = image.Id,
                    MimeType = image.FileType,
                    UniqueFileName = image.Id.ToString() + ".jpg"
                };
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
                    this.OwnerDetails = await this.dataService.FindOwnerBySystemIdAsync(ownerId);

                    if (this.OwnerDetails != null)
                    {
                        this.Title = "Edit " + this.OwnerDetails.FirstName;
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, " GetOwner Error");
            }
        }

        private async Task SaveOwnerDetails()
        {
            try
            {
                if (await UserDialogs.Instance.ConfirmAsync($"Are the following details correct:\n{this.OwnerDetails.ToString()}"))
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    if (this.OwnerDetails.IcasaPopPhoto != null)
                    {
                        if (this.OwnerDetails.IcasaPopPhoto?.Id == null || this.OwnerDetails.IcasaPopPhoto?.Id == Guid.Empty)
                        {
                            this.OwnerDetails.IcasaPopPhoto.UniqueFileName = this.OwnerDetails.IcasaPopPhoto.Id.ToString() + ".jpg";
                        }
                    }

                    if (this.OwnerDetails.IdentificationDocument != null)
                    {
                        if (this.OwnerDetails.IdentificationDocument?.Id == null || this.OwnerDetails.IdentificationDocument?.Id == Guid.Empty)
                        {
                            this.OwnerDetails.IdentificationDocument.UniqueFileName = this.OwnerDetails.IdentificationDocument.Id.ToString() + ".jpg";
                        }
                    }

                    if (this.OwnerDetails.SkippersLicenseImage != null)
                    {
                        if (this.OwnerDetails.SkippersLicenseImage?.Id == null || this.OwnerDetails.SkippersLicenseImage?.Id == Guid.Empty)
                        {
                            this.OwnerDetails.SkippersLicenseImage.UniqueFileName = this.OwnerDetails.SkippersLicenseImage.Id.ToString() + ".jpg";
                        }
                    }

                    if (this.OwnerDetails.Id == null || this.OwnerDetails.Id == Guid.Empty)
                    {
                        this.OwnerDetails.Id = await this.dataService.CreateNewOwnerAsync(this.OwnerDetails);
                        if (this.OwnerDetails.Id == null || this.OwnerDetails.Id == Guid.Empty)
                        {
                            if (this.apiService == null)
                            {
                                this.apiService = new ServiceCommunication();
                            }

                            UserDialogs.Instance.Toast("Successfully created Owner: " + this.OwnerDetails.FirstName);
                            Guid ownerId;

                            try
                            {
                                ownerId = await this.apiService.CreateOwner(this.OwnerDetails);
                            }
                            catch (WebException)
                            {
                                ownerId = Guid.Empty;
                            }

                            if (ownerId != null && ownerId != Guid.Empty)
                            {
                                SettingsService.OwnerId = ownerId.ToString();
                                this.OwnerDetails.IsSynced = true;
                                UserDialogs.Instance.Toast("Successfully created owner details on server.");
                            }
                            else
                            {
                                this.OwnerDetails.IsSynced = false;
                                await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.");
                            }

                            var syncResult = await this.dataService.UpdateOwnerAsync(this.OwnerDetails);

                            MessagingCenter.Instance.Send<OwnerMobileModel, string>(this.OwnerDetails, "Owner", "");

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Shell.Current.Navigation.PopAsync();
                            });
                        }
                    }
                    else
                    {
                        var result = await this.dataService.UpdateOwnerAsync(this.OwnerDetails);
                        if (result)
                        {
                            if (this.apiService == null)
                            {
                                this.apiService = new ServiceCommunication();
                            }

                            UserDialogs.Instance.Toast("Successfully updated Owner: " + this.OwnerDetails.FirstName);
                            Guid ownerId;

                            try
                            {
                                ownerId = await this.apiService.UpdateOwner(this.OwnerDetails);
                            }
                            catch (WebException)
                            {
                                ownerId = Guid.Empty;
                            }

                            if (ownerId != null && ownerId != Guid.Empty)
                            {
                                SettingsService.OwnerId = ownerId.ToString();
                                this.OwnerDetails.IsSynced = true;
                                UserDialogs.Instance.Toast("Successfully updated owner details to server.");
                            }
                            else
                            {
                                this.OwnerDetails.IsSynced = false;
                                await UserDialogs.Instance.AlertAsync("Could not update your details on the server. Please try again later.");
                            }

                            var syncResult = await this.dataService.UpdateOwnerAsync(this.OwnerDetails);

                            MessagingCenter.Instance.Send<OwnerMobileModel, string>(this.OwnerDetails, "Owner", "");
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Shell.Current.Navigation.PopAsync(true);
                            });
                        }
                    }
                }
            }
            catch (WebException webExc)
            {
                await UserDialogs.Instance.AlertAsync($"{webExc.Status} - {webExc.Message} - {webExc.Response}", "Save Owner Error");
                Crashes.TrackError(webExc);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message + " - " + exc.InnerException?.Message, "Save Owner Error");
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
