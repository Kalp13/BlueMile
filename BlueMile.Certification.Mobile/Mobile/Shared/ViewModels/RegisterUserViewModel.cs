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

        public string Email
        {
            get { return this.username; }
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    this.OnPropertyChanged(nameof(this.Email));
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

        public string ConfirmPassword
        {
            get { return this.confirmPassword; }
            set
            {
                if (this.confirmPassword != value)
                {
                    this.confirmPassword = value;
                    this.OnPropertyChanged(nameof(this.ConfirmPassword));
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
                var register = await App.ApiService.RegisterUser(new UserRegistrationModel()
                {
                    EmailAddress = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                }).ConfigureAwait(false);

                if (register)
                {
                    UserDialogs.Instance.Toast($"Successfully registered with username {this.Email}");
                    App.Current.MainPage = new LoginPage();
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

        private string username;

        private string password;

        private bool hidePassword;
        private string confirmPassword;

        #endregion
    }
}
