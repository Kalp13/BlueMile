using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Views;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class RegisterUserViewModel : BaseViewModel
    {
        #region Instance Properties

        public UserRegistrationModel UserRegistration
        {
            get { return this.userRegistration; }
            set
            {
                if (this.userRegistration != value)
                {
                    this.userRegistration = value;
                    this.OnPropertyChanged(nameof(this.UserRegistration));
                }
            }
        }

        public bool HidePassword
        {
            get { return this.hidePassword; }
            set
            {
                if (this.hidePassword != value)
                {
                    this.hidePassword = value;
                    this.OnPropertyChanged(nameof(this.HidePassword));
                }
            }
        }

        public ICommand ClearCommand
        {
            get;
            private set;
        }

        public ICommand RegisterCommand
        {
            get;
            private set;
        }

        public ICommand ClearDetailsCommand
        {
            get;
            private set;
        }

        public ICommand DisplayPasswordCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public RegisterUserViewModel()
        {
            this.HidePassword = true;
            this.InitCommands();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.ClearCommand = new Command(() =>
            {

            });
            this.RegisterCommand = new Command(async () =>
            {
                await this.RegisterUser().ConfigureAwait(false);
            });
            this.DisplayPasswordCommand = new Command(() =>
            {
                this.HidePassword = !this.HidePassword;
            });
        }

        private async Task RegisterUser()
        {
            try
            {
                var register = await App.ApiService.RegisterUser(this.UserRegistration).ConfigureAwait(false);

                if (register)
                {
                    UserDialogs.Instance.Toast($"Successfully registered with username {this.UserRegistration.EmailAddress}");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage = new LoginPage();
                    });
                }
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Registration Error");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

        #region Instance Fields

        private UserRegistrationModel userRegistration;

        private bool hidePassword;

        #endregion
    }
}
