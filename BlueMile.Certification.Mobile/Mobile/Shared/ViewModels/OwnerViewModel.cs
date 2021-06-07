using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class OwnerViewModel : BaseViewModel
    {
        #region Instance Properties

        public OwnerMobileModel CurrentOwner
        {
            get { return this.currentOwner; }
            set
            {
                if (this.currentOwner != value)
                {
                    this.currentOwner = value;
                    base.OnPropertyChanged(nameof(this.CurrentOwner));
                }
            }
        }

        public List<DocumentMobileModel> OwnerImages
        {
            get { return this.ownerImages; }
            set
            {
                if (this.ownerImages != value)
                {
                    this.ownerImages = value;
                    this.OnPropertyChanged(nameof(this.OwnerImages));
                }
            }
        }

        public ICommand EditOwnerCommand
        {
            get;
            private set;
        }

        public ICommand SyncCommand
        {
            get;
            private set;
        }

        public ImageSource MenuImage
        {
            get { return this.menuImage; }
            set
            {
                if (this.menuImage != value)
                {
                    this.menuImage = value;
                    this.OnPropertyChanged(nameof(this.MenuImage));
                }
            }
        }

        #endregion

        #region Constructor

        public OwnerViewModel()
        {
            this.InitCommands();

            this.CurrentOwner = new OwnerMobileModel();
            
            this.GetOwner().ConfigureAwait(false);
        }

        #endregion

        #region Class Methods

        public void InitCommands()
        {
            this.SyncCommand = new Command(async() =>
            {
                UserDialogs.Instance.ShowLoading("Syncing Owner...");
                await this.SyncOwnerDetails().ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            });
            this.EditOwnerCommand = new Command(async () =>
            {
                await this.EditOwnerDetails();
            });
        }

        private async Task EditOwnerDetails()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");

                MessagingCenter.Instance.Subscribe<OwnerMobileModel, string>(this.CurrentOwner, "Owner", (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await this.GetOwner();
                    });
                });
                await Shell.Current.Navigation.PushAsync(new CreateUpdateOwnerPage()).ConfigureAwait(false);

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Edit Owner Error");
            }
        }

        private async Task SyncOwnerDetails()
        {
            try
            {
                if (this.CurrentOwner != null)
                {
                    if (!this.CurrentOwner.IsSynced && this.CurrentOwner.SystemId == Guid.Empty)
                    {
                        if (this.apiService == null)
                        {
                            this.apiService = new ServiceCommunication();
                        }

                        var ownerId = await this.apiService.CreateOwner(this.CurrentOwner).ConfigureAwait(false);
                        if (ownerId != null && ownerId != Guid.Empty)
                        {
                            this.CurrentOwner.IsSynced = true;
                            UserDialogs.Instance.Toast("Successfully saved owner details to server.");
                        }
                        else
                        {
                            this.CurrentOwner.IsSynced = false;
                            await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.", "Create Error").ConfigureAwait(false);
                        }

                        if (this.dataService == null)
                        {
                            this.dataService = new DataService();
                        }
                        var syncResult = await this.dataService.UpdateOwnerAsync(this.CurrentOwner).ConfigureAwait(false);
                    }
                    else
                    {
                        if (this.apiService == null)
                        {
                            this.apiService = new ServiceCommunication();
                        }

                        var owner = await this.apiService.UpdateOwner(this.CurrentOwner).ConfigureAwait(false);
                        if (owner != null && owner != Guid.Empty)
                        {
                            this.CurrentOwner.IsSynced = true;
                            UserDialogs.Instance.Toast("Successfully updaed owner details on server.");
                        }
                        else
                        {
                            this.CurrentOwner.IsSynced = false;
                            await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.", "Create Error").ConfigureAwait(false);
                        }

                        if (this.dataService == null)
                        {
                            this.dataService = new DataService();
                        }
                        var syncResult = await this.dataService.UpdateOwnerAsync(this.CurrentOwner).ConfigureAwait(false);
                    }
                }
                else
                {
                    if (this.dataService == null)
                    {
                        this.dataService = new DataService();
                    }

                    this.CurrentOwner = (await this.dataService.FindOwnersAsync().ConfigureAwait(false)).FirstOrDefault();

                    if (this.CurrentOwner != null)
                    {
                        SettingsService.OwnerId = this.CurrentOwner.SystemId.ToString();
                        this.OwnerImages = new List<DocumentMobileModel>();
                        this.OwnerImages.Add(this.CurrentOwner.IcasaPopPhoto);
                        this.OwnerImages.Add(this.CurrentOwner.IdentificationDocument);
                        this.OwnerImages.Add(this.CurrentOwner.SkippersLicenseImage);

                        this.Title = String.Format(CultureInfo.InvariantCulture, "{0}'s Details", this.CurrentOwner.FirstName);
                        this.MenuImage = ImageSource.FromFile("edit.png");
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(SettingsService.OwnerId) && Guid.Parse(SettingsService.OwnerId) != Guid.Empty)
                        {
                            if (this.apiService == null)
                            {
                                this.apiService = new ServiceCommunication();
                            }

                            var onlineOwner = await this.apiService.GetOwnerBySystemId(Guid.Parse(SettingsService.OwnerId));
                            if (onlineOwner != null)
                            {
                                this.CurrentOwner = onlineOwner;
                                this.CurrentOwner.Id = await this.dataService.CreateNewOwnerAsync(onlineOwner);
                            }
                            else
                            {
                                this.Title = "No Owner Available";
                                this.MenuImage = ImageSource.FromFile("add.png");
                            }
                        }
                        else
                        {
                            this.Title = "No Owner Available";
                            this.MenuImage = ImageSource.FromFile("add.png");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.ToString(), "Sync Error").ConfigureAwait(false);
                Crashes.TrackError(exc);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task GetOwner()
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var owner = await this.dataService.FindOwnersAsync().ConfigureAwait(false);

                this.CurrentOwner = owner?.FirstOrDefault(x => x.SystemId == Guid.Parse(SettingsService.OwnerId));

                if (this.CurrentOwner != null)
                {
                    SettingsService.OwnerId = this.CurrentOwner.SystemId.ToString();
                    this.OwnerImages = new List<DocumentMobileModel>();
                    this.OwnerImages.Add(this.CurrentOwner.IcasaPopPhoto);
                    this.OwnerImages.Add(this.CurrentOwner.IdentificationDocument);
                    this.OwnerImages.Add(this.CurrentOwner.SkippersLicenseImage);

                    this.Title = String.Format(CultureInfo.InvariantCulture, "{0}'s Details", this.CurrentOwner.FirstName);
                    this.MenuImage = ImageSource.FromFile("edit.png");
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(SettingsService.OwnerId) && Guid.Parse(SettingsService.OwnerId) != Guid.Empty)
                    {
                        if (this.apiService == null)
                        {
                            this.apiService = new ServiceCommunication();
                        }

                        var onlineOwner = await this.apiService.GetOwnerBySystemId(Guid.Parse(SettingsService.OwnerId));
                        if (onlineOwner != null)
                        {
                            this.CurrentOwner = onlineOwner;
                            var localOwner = await this.dataService.FindOwnerBySystemIdAsync(onlineOwner.SystemId);
                            if (localOwner != null)
                            {
                                await this.dataService.UpdateOwnerAsync(this.CurrentOwner);
                            }
                            else
                            {
                                this.CurrentOwner.Id = await this.dataService.CreateNewOwnerAsync(onlineOwner);
                            }

                            this.Title = $"{this.CurrentOwner.FirstName}'s Details";
                            this.MenuImage = ImageSource.FromFile("edit.png");
                        }
                        else
                        {
                            this.Title = "No Owner Available";
                            this.MenuImage = ImageSource.FromFile("add.png");
                        }
                    }
                    else
                    {
                        this.Title = "No Owner Available";
                        this.MenuImage = ImageSource.FromFile("add.png");
                    }
                }
            }
            catch (Exception exc)
            {
                this.Title = "No Owner Available";
                this.MenuImage = ImageSource.FromFile("add.png");
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.ToString(), "Load Owner Error");
            }
        }

        #endregion

        #region Instance Fields

        private OwnerMobileModel currentOwner;

        private List<DocumentMobileModel> ownerImages;

        private ImageSource menuImage;

        private IDataService dataService;

        private IServiceCommunication apiService;

        #endregion
    }
}
