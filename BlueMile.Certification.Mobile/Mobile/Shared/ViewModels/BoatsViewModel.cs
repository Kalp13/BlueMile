using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class BoatsViewModel : BaseViewModel
    {
        #region Implementation Properties

        public ObservableCollection<BoatMobileModel> OwnersBoats
        {
            get { return this.ownersBoats; }
            set
            {
                if (this.ownersBoats != value)
                {
                    this.ownersBoats = value;
                    this.OnPropertyChanged(nameof(this.OwnersBoats));
                }
            }
        }

        public BoatMobileModel SelectedBoat
        {
            get { return this.selectedBoat; }
            set
            {
                if (this.selectedBoat != value)
                {
                    this.selectedBoat = value;
                    this.OnPropertyChanged(nameof(this.SelectedBoat));

                    this.OpenBoatDetail();
                }
            }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    this.OnPropertyChanged(nameof(this.IsRefreshing));
                }
            }
        }

        public ICommand NewBoatCommand
        {
            get;
            private set;
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }

        public ICommand SyncBoatsCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public BoatsViewModel()
        {
            this.Title = "Boats";
            this.InitCommands();

            if (!String.IsNullOrWhiteSpace(SettingsService.OwnerId))
            {
                this.GetBoats().ConfigureAwait(false);
            }
            else
            {
                UserDialogs.Instance.Alert("You have not entered your details as an owner.\n Please update them now to continue.", "No Owner Details", "Ok");
            }
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.NewBoatCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                await AddNewBoat().ConfigureAwait(false);
            }, () => !String.IsNullOrWhiteSpace(SettingsService.OwnerId));
            this.RefreshCommand = new Command(async () =>
            {
                this.IsRefreshing = true;
                await this.GetBoats().ConfigureAwait(false);
                this.IsRefreshing = false;
            });
            this.SyncBoatsCommand = new Command(async () =>
            {
                await this.SyncBoatsToServer();
            });
        }

        private async Task SyncBoatsToServer()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Syncing...");

                foreach(var boat in this.OwnersBoats)
                {
                    await this.SaveBoatDetails(boat);
                }

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Sync Error");
            }
        }

        private async Task AddNewBoat()
        {
            try
            {
                MessagingCenter.Instance.Subscribe<string, string>("Add", "Boat", (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        this.IsRefreshing = true;
                        await this.GetBoats().ConfigureAwait(false);
                        this.IsRefreshing = false;
                    });
                });
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.boatEditRoute}?boatId={Guid.Empty}", true).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Add Boat Error").ConfigureAwait(false);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task GetBoats()
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var owner = Guid.Parse(SettingsService.OwnerId);
                this.OwnersBoats = new ObservableCollection<BoatMobileModel>(await this.dataService.FindBoatsByOwnerIdAsync(owner).ConfigureAwait(false));
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Boats Error").ConfigureAwait(false);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void OpenBoatDetail()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.boatDetailRoute}?boatId={this.SelectedBoat.SystemId}").ConfigureAwait(false);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Open Boat Error", "Ok").ConfigureAwait(false);
            }
        }

        private async Task SaveBoatDetails(BoatMobileModel boat)
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                boat.OwnerId = Guid.Parse(SettingsService.OwnerId);

                if (!boat.IsJetski)
                {
                    boat.TubbiesCertificateNumber = String.Empty;

                    if (boat.TubbiesCertificateImage != null)
                    {
                        boat.TubbiesCertificateImage.FilePath = String.Empty;
                        boat.TubbiesCertificateImage.ImageName = String.Empty;
                        boat.TubbiesCertificateImage.UniqueImageName = String.Empty;
                    }
                }

                if (boat.BoyancyCertificateImage != null)
                {
                    if (boat.BoyancyCertificateImage.Id == null || boat.BoyancyCertificateImage.Id == Guid.Empty)
                    {
                        boat.BoyancyCertificateImage.Id = Guid.NewGuid();
                        boat.BoyancyCertificateImage.UniqueImageName = boat.BoyancyCertificateImage.Id.ToString() + ".jpg";
                    }
                }

                if (boat.IsJetski && boat.TubbiesCertificateImage != null)
                {
                    if (boat.TubbiesCertificateImage.Id == null || boat.TubbiesCertificateImage.Id == Guid.Empty)
                    {
                        boat.TubbiesCertificateImage.Id = Guid.NewGuid();
                        boat.TubbiesCertificateImage.UniqueImageName = boat.TubbiesCertificateImage.Id.ToString() + ".jpg";
                    }
                }

                if (boat.BoatCategoryId <= 0)
                {
                    await UserDialogs.Instance.AlertAsync("Please select the category of the boat.", "Incomplete Boat").ConfigureAwait(false);
                }
                else
                {
                    boat.Id = await this.dataService.CreateNewBoatAsync(boat).ConfigureAwait(false);
                    if (boat.Id > 0)
                    {
                        if (this.apiService == null)
                        {
                            this.apiService = new ServiceCommunication();
                        }

                        UserDialogs.Instance.Toast($"Successfulle saved {boat.Name}");

                        if (boat.SystemId == Guid.Empty)
                        {
                            var boatId = await this.apiService.CreateBoat(boat).ConfigureAwait(false);
                            boat.SystemId = boatId;
                        }
                        else
                        {
                            var boatId = await this.apiService.UpdateBoat(boat).ConfigureAwait(false);
                            boat.SystemId = boatId;
                        }

                        var syncResult = await this.dataService.UpdateBoatAsync(boat).ConfigureAwait(false);
                        UserDialogs.Instance.Toast($"Successfully uploaded {boat.Name}");
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Could not successfully save this boat. Please try again.", "Unsuccessfully Saved", "Ok").ConfigureAwait(false);
                    }
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Saving Boat Error").ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

            #region Instance Fields

        private ObservableCollection<BoatMobileModel> ownersBoats;

        private BoatMobileModel selectedBoat;

        private bool isRefreshing;

        private IDataService dataService;

        private IServiceCommunication apiService;

        #endregion
    }
}
