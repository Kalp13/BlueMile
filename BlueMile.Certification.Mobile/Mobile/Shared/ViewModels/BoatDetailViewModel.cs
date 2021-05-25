using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Converters;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    [QueryProperty("CurrentBoatId", "boatId")]
    public class BoatDetailViewModel : BaseViewModel
    {
        #region Instance Properties

        public string CurrentBoatId
        {
            get { return this.currentBoatId; }
            set
            {
                this.currentBoatId = Uri.UnescapeDataString(value);
                this.GetBoat().ConfigureAwait(false);
            }
        }

        public BoatMobileModel CurrentBoat
        {
            get { return this.currentBoat; }
            set
            {
                if (this.currentBoat != value)
                {
                    this.currentBoat = value;
                    this.OnPropertyChanged(nameof(this.CurrentBoat));
                }
            }
        }

        public ObservableCollection<ImageMobileModel> BoatImages
        {
            get { return this.boatImages; }
            set
            {
                if (this.boatImages != value)
                {
                    this.boatImages = value;
                    this.OnPropertyChanged(nameof(this.BoatImages));
                }
            }
        }

        public bool EditBoat
        {
            get { return this.editBoat; }
            set
            {
                if (this.editBoat != value)
                {
                    this.editBoat = value;
                    this.OnPropertyChanged(nameof(editBoat));
                }
            }
        }

        public ICommand EquipmentListCommand
        {
            get;
            private set;
        }

        public ICommand ViewRequirementsCommand
        {
            get;
            private set;
        }

        public ICommand SubmitForCertificationCommand
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

        public ICommand EditBoatCommand
        {
            get;
            private set;
        }

        public ICommand SyncCommand
        {
            get;
            private set;
        }

        public ICommand RequestCertificateCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public BoatDetailViewModel()
        {
            this.Title = "Boat Detail";
            this.InitCommands();
            if (!String.IsNullOrWhiteSpace(this.CurrentBoatId))
            {
                this.GetBoat().ConfigureAwait(false);
            }
            
            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.EquipmentListCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                await Shell.Current.GoToAsync($"{Constants.itemsRoute}?boatId={this.CurrentBoat.SystemId}").ConfigureAwait(false);
                Shell.Current.FlyoutIsPresented = false;
            });
            this.ViewRequirementsCommand = new Command(async () =>
            {
                await UserDialogs.Instance.AlertAsync(await RequirementValidationService.GetRequiredItems(this.CurrentBoat.SystemId).ConfigureAwait(false)).ConfigureAwait(false);
            });
            this.SubmitForCertificationCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Submitting...");
                await this.BuildEmailContent().ConfigureAwait(false);
            });

            this.SaveCommand = new Command(async () =>
            {
                await this.SaveBoatDetails();
            });
            this.CancelCommand = new Command(async () =>
            {
                if (await UserDialogs.Instance.ConfirmAsync("Are you sure you want to cancel capturing this boat?", "Cancel Boat?", "Yes", "No").ConfigureAwait(false))
                {
                    this.EditBoat = false;
                    await this.GetBoat().ConfigureAwait(false);
                }
            });

            this.EditBoatCommand = new Command(async () =>
            {
                this.EditBoat = true;
                UserDialogs.Instance.ShowLoading("Loading...");
                var destinationRoute = "boats/update";
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{destinationRoute}?boatId={this.CurrentBoat.SystemId}").ConfigureAwait(false);
                Shell.Current.FlyoutIsPresented = false;
            });

            this.SyncCommand = new Command(async () =>
            {
                await this.UploadItemToServer().ConfigureAwait(false);
            });

            this.RequestCertificateCommand = new Command(async () =>
            {
                await this.RequestNewBoatCOF();
            });
        }

        private Task RequestNewBoatCOF()
        {
            throw new NotImplementedException();
        }

        private async Task UploadItemToServer()
        {
            try
            {
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                if (this.CurrentBoat.SystemId == null || this.CurrentBoat.SystemId == Guid.Empty)
                {
                    var boatId = await this.apiService.CreateBoat(this.CurrentBoat).ConfigureAwait(false);
                    if (boatId != null && boatId != Guid.Empty)
                    {
                        this.CurrentBoat.SystemId = boatId;
                        this.CurrentBoat.IsSynced = true;
                    }
                    else
                    {
                        this.CurrentBoat.IsSynced = false;
                    }
                }
                else
                {
                    var boatId = await this.apiService.UpdateBoat(this.CurrentBoat).ConfigureAwait(false);

                    if (boatId != null && boatId != Guid.Empty)
                    {
                        this.CurrentBoat.SystemId = boatId;
                        this.CurrentBoat.IsSynced = true;
                    }
                    else
                    {
                        this.CurrentBoat.IsSynced = false;
                    }
                }

                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var syncResult = await this.dataService.UpdateBoatAsync(this.CurrentBoat).ConfigureAwait(false);
                UserDialogs.Instance.Toast($"Successfully uploaded {this.CurrentBoat.Name}");
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Sync Error");
            }
        }

        private async Task SaveBoatDetails()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Saving...");
                this.CurrentBoat.OwnerId = Guid.Parse(SettingsService.OwnerId);

                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                if (this.CurrentBoat.BoatCategoryId <= 0)
                {
                    await UserDialogs.Instance.AlertAsync("Please select the category of the boat.", "Incomplete Boat").ConfigureAwait(false);
                }
                else if (await this.dataService.UpdateBoatAsync(this.CurrentBoat).ConfigureAwait(false))
                {
                    UserDialogs.Instance.Toast("Successfully updated " + this.CurrentBoat.Name);
                    this.EditBoat = false;
                    await this.GetBoat().ConfigureAwait(false);
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Could not successfully save this boat. Please try again.", "Unsuccessfully Saved", "Ok").ConfigureAwait(false);
                }
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Save Details Error");
            }
        }

        private async Task BuildEmailContent()
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var owner = await this.dataService.FindOwnerBySystemIdAsync(this.CurrentBoat.OwnerId).ConfigureAwait(false);
                var equipment = await this.dataService.FindItemsByBoatIdAsync(this.CurrentBoat.SystemId).ConfigureAwait(false);
                var images = new List<ImageMobileModel>();

                //Add Owner Details
                var details = $"Owner Details\n" +
                $"Name & Surname: {owner.Name} + {owner.Surname}\n" +
                $"Cell Number: {owner.ContactNumber}\n" +
                $"Email: {owner.Email}\n" +
                $"ID/Passport: {owner.Identification}\n" +
                $"Skippers License: {owner.SkippersLicenseNumber}\n" +
                $"VHF License: {owner.VhfOperatorsLicense}\n" +
                $"Address: {owner.AddressLine1}\n\n"; ;
                images.Add(owner.IdentificationDocument);
                images.Add(owner.SkippersLicenseImage);
                images.Add(owner.IcasaPopPhoto);

                ///Add Boat Details
                details += $"Boat Detail\n" +
                $"Boat Name: {this.CurrentBoat.Name}\n" +
                $"Category: {CategoryDescriptionConverter.GetDescription((BoatCategoryEnum)this.CurrentBoat.BoatCategoryId)}" +
                $"Registered Number: {this.CurrentBoat.RegisteredNumber}\n" +
                $"Boyancy Cert: {this.CurrentBoat.BoyancyCertificateNumber}\n";
                images.Add(this.CurrentBoat.BoyancyCertificateImage);

                if (this.CurrentBoat.IsJetski)
                {
                    details += $"Tubbies Boyancy Cert: {this.CurrentBoat.TubbiesCertificateNumber}\n";
                    images.Add(this.CurrentBoat.TubbiesCertificateImage);
                }

                //Add Equipment Details
                foreach (var item in equipment)
                {
                    details += $"{ItemTypeDescriptionConverter.GetDescription((ItemTypeEnum)item.ItemTypeId)}: {item.Description} - " +
                    $"Serial: {item.SerialNumber}" +
                    $"Captured: {item.CapturedDate.ToString("00:dd/MMM/yyyy")} - " +
                    $"Expires: {item.ExpiryDate.ToString("00:dd/MMM/yyyy")}";
                    images.Add(item.ItemImage);
                }

                this.BuildEmail(details, images);
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Create Email Error");
            }
        }

        private async void BuildEmail(string details, List<ImageMobileModel> images)
        {
            try
            {
                EmailMessage email = new EmailMessage()
                {
                    To = new List<string>() { "rfkalp@live.co.za" },
                    Body = "Dear Stephan,\n\n" +
                       "Please find attached and below everything required for my boats C.O.C.\n\n" + details,
                    BodyFormat = EmailBodyFormat.Html,
                    Subject = this.CurrentBoat.RegisteredNumber + " C.O.C"
                };
                List<EmailAttachment> attachments = new List<EmailAttachment>();
                foreach (var pic in images)
                {
                    attachments.Add(new EmailAttachment(pic.FilePath)
                    {
                        ContentType = "image/png"
                    });
                }
                email.Attachments = attachments;

                await Email.ComposeAsync(email).ConfigureAwait(false);
            }
            catch (FeatureNotSupportedException)
            {
                await UserDialogs.Instance.AlertAsync(new AlertConfig
                {
                    Message = "Your device does not support sending emails.",
                    OkText = "Close",
                    Title = "Not Supported"
                }).ConfigureAwait(false);
            }
            catch (FeatureNotEnabledException)
            {
                await UserDialogs.Instance.AlertAsync(new AlertConfig
                {
                    Message = "You have not enabled allowed this feature yet.",
                    OkText = "Close",
                    Title = "Not Supported"
                }).ConfigureAwait(false);
            }
        }

        private async Task GetBoat()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(this.CurrentBoatId))
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    this.CurrentBoat = await this.dataService.FindBoatBySystemIdAsync(Guid.Parse(this.CurrentBoatId)).ConfigureAwait(false);
                    this.BoatImages = new ObservableCollection<ImageMobileModel>();

                    if (this.CurrentBoat != null)
                    {
                        this.Title = this.CurrentBoat.Name;
                        this.BoatImages.Add(this.CurrentBoat.BoyancyCertificateImage);
                        this.BoatImages.Add(this.CurrentBoat.TubbiesCertificateImage);
                    }

                    this.EditBoat = false;
                }
                else
                {
                    this.EditBoat = true;
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Boat Error").ConfigureAwait(false);
            }
        }

        #endregion

        #region Instance Fields

        private BoatMobileModel currentBoat;

        private string currentBoatId;

        private ObservableCollection<ImageMobileModel> boatImages;

        private bool editBoat;

        private IDataService dataService;

        private IServiceCommunication apiService;

        #endregion
    }
}
