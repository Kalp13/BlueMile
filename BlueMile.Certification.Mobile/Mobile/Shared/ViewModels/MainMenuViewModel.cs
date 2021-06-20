using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Services;
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        #region Instance Properties

        public ICommand BoatsCommand
        {
            get;
            private set;
        }

        public ICommand CertificationsCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public MainMenuViewModel()
        {
            this.InitCommands();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.BoatsCommand = new Command(async () =>
            {
                await this.OpenBoatListAsync();
            });
            this.CertificationsCommand = new Command(async () =>
            {
                await this.GoToCertificationRequests();
            });
        }

        private async Task GoToCertificationRequests()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.allCertificationRequestsRoute}", true);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Navigation Error");
            }
        }

        private async Task OpenBoatListAsync()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.boatsRoute}", true);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Open Boat Error", "Ok");
            }
        }

        #endregion

        #region Instance Fields



        #endregion
    }
}
