using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Instance Properties

        public string Username
        {
            get { return this.username; }
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    this.OnPropertyChanged(nameof(this.Username));
                }
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    this.OnPropertyChanged(nameof(this.Password));
                }
            }
        }

        public ICommand LoginCommand
        {
            get;
            private set;
        }

        public ICommand LogoutCommand
        {
            get;
            private set;
        }

        public ICommand ClearDetailsCommand
        {
            get;
            private set;
        }

        public ICommand ForgotPasswordCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            this.InitCommands();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.LoginCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Logging In...");
                await this.LogUserIn().ConfigureAwait(false);
            });
            this.LogoutCommand = new Command(async () =>
            {
                await this.LogUserOut().ConfigureAwait(false);
            });
            this.ClearDetailsCommand = new Command(() =>
            {
                this.Username = String.Empty;
                this.Password = String.Empty;
            });
            this.ForgotPasswordCommand = new Command(async () =>
            {
                await this.ForgotPassword().ConfigureAwait(false);
            });
        }

        private Task LogUserOut()
        {
            throw new NotImplementedException();
        }

        private Task ForgotPassword()
        {
            throw new NotImplementedException();
        }

        private async Task LogUserIn()
        {
            try
            {
                var login = await App.ApiService.LogUserIn(this.Username, this.Password).ConfigureAwait(false);
                if (login != null)
                {
                    UserDialogs.Instance.Toast("Successfully logged in " + login.Name);
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Log In Error").ConfigureAwait(false);
            }
        }

        #endregion

        #region Instance Fields

        private string username;
        private string password;

        #endregion
    }
}
