using Acr.UserDialogs;
using BlueMile.Coc.Mobile.Models;
using BlueMile.Coc.Mobile.Services;
using BlueMile.Coc.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.ViewModels
{
    [QueryProperty("OwnerId", "ownerid")]
    public class BoatsViewModel : BaseViewModel
    {
        #region Implementation Properties

        //public Guid OwnerId
        //{
        //    get { return this.ownerId; }
        //    set
        //    {
        //        if (this.ownerId != value)
        //        {
        //            this.ownerId = Guid.Parse(Uri.UnescapeDataString(value.ToString()));
        //        }
        //    }
        //}

        public ObservableCollection<BoatModel> OwnersBoats
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

        public BoatModel SelectedBoat
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

        #endregion

        #region Constructor

        public BoatsViewModel()
        {
            this.Title = "Boats";
            this.InitCommands();
            
            if ((App.OwnerId == Guid.Empty) || (App.OwnerId == null))
            {
                if (!String.IsNullOrWhiteSpace(SettingsService.OwnerId))
                {
                    App.OwnerId = Guid.Parse(SettingsService.OwnerId);
                }
                else
                {
                    App.OwnerId = Guid.Empty;
                }
            }
            if ((App.OwnerId != Guid.Empty) && (App.OwnerId != null))
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
            }, () => App.OwnerId != Guid.Empty);
            this.RefreshCommand = new Command(async () =>
            {
                this.IsRefreshing = true;
                await this.GetBoats().ConfigureAwait(false);
                this.IsRefreshing = false;
            });
        }

        private async Task AddNewBoat()
        {
            var destinationRoute = "boats/new";
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{destinationRoute}?ownerId={App.OwnerId}", true).ConfigureAwait(false);
        }

        private async Task GetBoats()
        {
            try
            {
                this.OwnersBoats = new ObservableCollection<BoatModel>(await App.DataService.GetAllBoats(App.OwnerId).ConfigureAwait(false));
                //this.OwnerId = App.OwnerId;
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Get Boats Error").ConfigureAwait(false);
            }
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }

        private async void OpenBoatDetail()
        {
            UserDialogs.Instance.ShowLoading("Loading...");
            var destinationRoute = "boats/detail";
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{destinationRoute}?boatId={this.SelectedBoat.Id}").ConfigureAwait(false);
            Shell.Current.FlyoutIsPresented = false;
        }

        #endregion

        #region Instance Fields

        private ObservableCollection<BoatModel> ownersBoats;

        private BoatModel selectedBoat;

        private Guid ownerId;

        private bool isRefreshing;

        #endregion
    }
}
