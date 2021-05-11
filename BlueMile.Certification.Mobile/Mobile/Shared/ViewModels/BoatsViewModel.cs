using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
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
        }

        private async Task AddNewBoat()
        {
            try
            {
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
                await Shell.Current.GoToAsync($"{nameof(CreateUpdateBoatPage)}?boatId={this.SelectedBoat.SystemId}").ConfigureAwait(false);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Open Boat Error", "Ok").ConfigureAwait(false);
            }
        }

        #endregion

        #region Instance Fields

        private ObservableCollection<BoatMobileModel> ownersBoats;

        private BoatMobileModel selectedBoat;

        private bool isRefreshing;

        private IDataService dataService;

        #endregion
    }
}
