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
        }

        private async Task OpenBoatListAsync()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.boatsRoute}");
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
