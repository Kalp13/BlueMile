using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Converters;
using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.InternalServices;
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
                var destinationRoute = "items";
                await Shell.Current.GoToAsync($"{destinationRoute}?boatId={CurrentBoat.Id.ToString()}").ConfigureAwait(false);
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
                UserDialogs.Instance.ShowLoading("Saving...");
                this.CurrentBoat.OwnerId = App.OwnerId;

                if (this.CurrentBoat.BoatCategoryId <= 0)
                {
                    await UserDialogs.Instance.AlertAsync("Please select the category of the boat.", "Incomplete Boat").ConfigureAwait(false);
                }
                else if (await App.DataService.UpdateBoatAsync(CurrentBoat).ConfigureAwait(false))
                {
                    UserDialogs.Instance.Toast("Successfully updated " + this.CurrentBoat.Name, TimeSpan.FromSeconds(2));
                    this.EditBoat = false;
                    await this.GetBoat().ConfigureAwait(false);
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Could not successfully save this boat. Please try again.", "Unsuccessfully Saved", "Ok").ConfigureAwait(false);
                }
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
                await Shell.Current.GoToAsync($"{destinationRoute}?boatId={this.CurrentBoat.Id}").ConfigureAwait(false);
                Shell.Current.FlyoutIsPresented = false;
            });
        }

        private async Task BuildEmailContent()
        {
            var owner = await App.DataService.GetOwnerById(this.CurrentBoat.OwnerId).ConfigureAwait(false);
            var equipment = await App.DataService.GetItemsByBoatId(this.CurrentBoat.Id).ConfigureAwait(false);
            var images = new List<ImageMobileModel>();

            //Add Owner Details
            var details = $"Owner Details\n" +
            $"Name & Surname: {owner.Name} + {owner.Surname}\n" +
            $"Cell Number: {owner.CellNumber}\n" +
            $"Email: {owner.Email}\n" +
            $"ID/Passport: {owner.IdentificationNumber}\n" +
            $"Skippers License: {owner.SkippersLicenseNumber}\n" +
            $"VHF License: {owner.VhfOperatorsLicense}\n" +
            $"Address: {owner.Address}\n\n"; ;
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
                details += $"{ItemTypeDescriptionConverter.GetDescription(item.ItemTypeId)}: {item.Description} - " +
                $"Serial: {item.SerialNumber}" +
                $"Captured: {item.CapturedDate.ToString("00:dd/MMM/yyyy")} - " +
                $"Expires: {item.ExpiryDate.ToString("00:dd/MMM/yyyy")}";
                images.Add(item.ItemImage);
            }

            this.BuildEmail(details, images);
        }

        private async void BuildEmail(string details, List<ImageModel> images)
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
                    this.CurrentBoat = await App.DataService.FindBoatBySystemIdAsync(Guid.Parse(this.CurrentBoatId)).ConfigureAwait(false);
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

        #endregion
    }
}
