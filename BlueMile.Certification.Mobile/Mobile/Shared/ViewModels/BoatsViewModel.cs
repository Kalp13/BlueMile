using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
                await this.SyncBoatsWithServer();
            });
        }

        private async Task SyncBoatsWithServer()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Syncing...");

                foreach(var boat in this.OwnersBoats.Where(x => !x.IsSynced))
                {
                    await this.SaveBoatDetailsToServer(boat);
                }

                await this.GetBoatsFromServer();

                await this.GetBoats();
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Sync Error");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task GetBoatsFromServer()
        {
            try
            {
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                var boats = await this.apiService.GetBoatsByOwnerId(Guid.Parse(SettingsService.OwnerId));
                
                foreach (var boat in boats)
                {
                    await this.SaveBoatDetailsLocally(boat);
                }
            }
            catch (WebException webExc)
            {
                await UserDialogs.Instance.AlertAsync($"Could not retrieve boats from server: {webExc.Message}").ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SaveBoatDetailsLocally(BoatMobileModel boat)
        {
            try
            {
                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                var exists = await this.dataService.FindBoatByIdAsync(boat.Id);
                boat.IsSynced = true;

                if (exists == null)
                {
                    if (boat.BoyancyCertificateImage != null)
                    {
                        boat.BoyancyCertificateImage.FilePath = Path.Combine(FileSystem.CacheDirectory, boat.BoyancyCertificateImage.UniqueFileName);
                        await File.WriteAllBytesAsync(boat.BoyancyCertificateImage.FilePath, boat.BoyancyCertificateImage.FileContent);
                    }
                    if (boat.TubbiesCertificateImage != null)
                    {
                        boat.TubbiesCertificateImage.FilePath = Path.Combine(FileSystem.CacheDirectory, boat.TubbiesCertificateImage.UniqueFileName);
                        await File.WriteAllBytesAsync(boat.TubbiesCertificateImage.FilePath, boat.TubbiesCertificateImage.FileContent);
                    }

                    boat.Id = await this.dataService.CreateNewBoatAsync(boat);
                }
                else if (await UserDialogs.Instance.ConfirmAsync($"Would you like to replace {exists.ToString()} with \n{boat.ToString()}"))
                {
                    if (boat.BoyancyCertificateImage != null)
                    {
                        boat.BoyancyCertificateImage.FilePath = Path.Combine(FileSystem.CacheDirectory, boat.BoyancyCertificateImage.UniqueFileName);
                        await File.WriteAllBytesAsync(boat.BoyancyCertificateImage.FilePath, boat.BoyancyCertificateImage.FileContent);
                    }
                    if (boat.TubbiesCertificateImage != null)
                    {
                        boat.TubbiesCertificateImage.FilePath = Path.Combine(FileSystem.CacheDirectory, boat.TubbiesCertificateImage.UniqueFileName);
                        await File.WriteAllBytesAsync(boat.TubbiesCertificateImage.FilePath, boat.TubbiesCertificateImage.FileContent);
                    }

                    await this.dataService.UpdateBoatAsync(boat);
                }
            }
            catch (Exception)
            {
                throw;
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

                var ownerId = Guid.Parse(SettingsService.OwnerId);
                this.OwnersBoats = new ObservableCollection<BoatMobileModel>(await this.dataService.FindBoatsByOwnerIdAsync(ownerId).ConfigureAwait(false));
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
                await Shell.Current.GoToAsync($"{Constants.boatDetailRoute}?boatId={this.SelectedBoat.Id}").ConfigureAwait(false);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Open Boat Error", "Ok").ConfigureAwait(false);
            }
        }

        private async Task SaveBoatDetailsToServer(BoatMobileModel boat)
        {
            try
            {
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }
                var doesExist = await this.apiService.DoesBoatExist(boat.Id);

                if (!doesExist)
                {
                    Guid boatId;
                    try
                    {
                        boatId = await this.apiService.CreateBoat(boat);
                    }
                    catch (WebException)
                    {
                        boatId = Guid.Empty;
                    }
                    if (boatId != null && boatId != Guid.Empty)
                    {
                        boat.Id = boatId;
                        boat.IsSynced = true;
                    }
                    else
                    {
                        boat.IsSynced = false;
                    }
                }
                else
                {
                    Guid boatId;
                    try
                    {
                        boatId = await this.apiService.UpdateBoat(boat);
                    }
                    catch (WebException)
                    {
                        boatId = Guid.Empty;
                    }

                    if (boatId != null && boatId != Guid.Empty)
                    {
                        boat.Id = boatId;
                        boat.IsSynced = true;

                        UserDialogs.Instance.Toast($"Successfully uploaded {boat.Name}");
                    }
                    else
                    {
                        boat.IsSynced = false;
                    }
                }

                if (this.dataService == null)
                {
                    this.dataService = new DataService();
                }

                if (boat.Id == null || boat.Id == Guid.Empty)
                {
                    boat.Id = await this.dataService.CreateNewBoatAsync(boat).ConfigureAwait(false);
                }
                else
                {
                    await this.dataService.UpdateBoatAsync(boat).ConfigureAwait(false);
                }
            }
            catch (WebException webExc)
            {
                await UserDialogs.Instance.AlertAsync($"Could not upload boat to server: {webExc.Message}").ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Saving Boat Error").ConfigureAwait(false);
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
