using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Helpers;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using BlueMile.Certification.Web.ApiModels.Helper;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public List<ImageMobileModel> OwnerImages
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
                UserDialogs.Instance.ShowLoading("Loading...");
                await Shell.Current.Navigation.PushAsync(new CreateUpdateOwnerPage()).ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            });
        }

        private async Task SyncOwnerDetails()
        {
            try
            {
                if (!this.CurrentOwner.IsSynced || this.CurrentOwner.SystemId == Guid.Empty)
                {
                    var owner = await App.ApiService.CreateOwner(OwnerHelper.ToCreateOwnerModel(OwnerModelHelper.ToOwnerModel(this.CurrentOwner))).ConfigureAwait(false);
                    if (owner != null)
                    {
                        this.CurrentOwner.IsSynced = true;
                        var syncResult = await App.DataService.UpdateOwnerAsync(this.CurrentOwner).ConfigureAwait(false);
                        UserDialogs.Instance.Toast("Successfully saved owner details to server.", TimeSpan.FromSeconds(5));
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.", "Create Error").ConfigureAwait(false);
                    }
                }
                else
                {
                    var owner = await App.ApiService.UpdateOwner(OwnerHelper.ToUpdateOwnerModel(OwnerModelHelper.ToOwnerModel(this.CurrentOwner))).ConfigureAwait(false);
                    if (owner != null)
                    {
                        this.CurrentOwner.IsSynced = true;
                        var syncResult = await App.DataService.UpdateOwnerAsync(this.CurrentOwner).ConfigureAwait(false);
                        UserDialogs.Instance.Toast("Successfully saved owner details to server.", TimeSpan.FromSeconds(5));
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Could not upload your details. Please try again later.", "Create Error").ConfigureAwait(false);
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
                this.CurrentOwner = (await App.DataService.FindOwnersAsync().ConfigureAwait(false)).FirstOrDefault();

                if (this.CurrentOwner != null)
                {
                    App.OwnerId = this.CurrentOwner.SystemId;
                    SettingsService.OwnerId = this.CurrentOwner.Id.ToString();
                    this.OwnerImages = new List<ImageMobileModel>();
                    this.OwnerImages.Add(this.CurrentOwner.IcasaPopPhoto);
                    this.OwnerImages.Add(this.CurrentOwner.IdentificationDocument);
                    this.OwnerImages.Add(this.CurrentOwner.SkippersLicenseImage);

                    this.Title = String.Format(CultureInfo.InvariantCulture, "{0}'s Details", this.CurrentOwner.Name);
                    this.MenuImage = ImageSource.FromFile("edit.png");
                }
                else
                {
                    this.Title = "No Owner Available";
                    this.MenuImage = ImageSource.FromFile("add.png");
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

        private List<ImageMobileModel> ownerImages;

        private ImageSource menuImage;

        #endregion
    }
}
