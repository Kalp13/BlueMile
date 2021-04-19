using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Views;
using BlueMile.Certification.Web.ApiModels;
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

        public ICommand DisplayPasswordCommand
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
            this.RegisterCommand = new Command(async () =>
            {
                await Shell.Current.Navigation.PushAsync(new RegisterUserPage()).ConfigureAwait(false);
            });
            this.DisplayPasswordCommand = new Command(() =>
            {
                this.HidePassword = !this.HidePassword;
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
                var login = await App.ApiService.LogUserIn(new UserLoginModel()
                {
                    EmailAddress = this.Username,
                    Password = this.Password
                }).ConfigureAwait(false);

                var owner = await App.ApiService.GetOwnerBySystemId(login);

                if (login != null)
                {
                    UserDialogs.Instance.Toast($"Successfully logged in {owner.Name} {owner.Surname}");
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

        private bool hidePassword;

        #endregion
    }
}
