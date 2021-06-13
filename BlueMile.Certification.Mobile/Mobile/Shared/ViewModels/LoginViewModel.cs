using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.AppCenter.Crashes;
using System;
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
                    this.RefreshCanExecutes();
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
                    this.RefreshCanExecutes();
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
            this.HidePassword = true;

#if DEBUG
            this.Username = "rfkalp@live.co.za";
            this.Password = "Password@1";
#endif
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.LoginCommand = new Command(async () =>
            {
                await this.LogUserIn();
            }, () => (!String.IsNullOrWhiteSpace(this.Username) && !String.IsNullOrWhiteSpace(this.Password)));
            this.LogoutCommand = new Command(async () =>
            {
                await this.LogUserOut().ConfigureAwait(false);
            });
            this.ClearDetailsCommand = new Command(async () =>
            {
                await this.ClearUserDetails().ConfigureAwait(false);
            });
            this.ForgotPasswordCommand = new Command(async () =>
            {
                await this.ForgotPassword().ConfigureAwait(false);
            });
            this.RegisterCommand = new Command(() =>
            {
                App.Current.MainPage = new RegisterUserPage();
            });
            this.DisplayPasswordCommand = new Command(() =>
            {
                this.HidePassword = !this.HidePassword;
            });
        }

        private async Task ClearUserDetails()
        {
            try
            {
                this.Username = String.Empty;
                this.Password = String.Empty;
                
                if (await UserDialogs.Instance.ConfirmAsync("Would you like to clear all the user details stored?", "Clear User Details", "Yes", "No").ConfigureAwait(false))
                {
                    SettingsService.Username = String.Empty;
                    SettingsService.Password = String.Empty;
                    SettingsService.OwnerId = String.Empty;
                    SettingsService.UserToken = String.Empty;
                }
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync(exc.Message, "Clearing User Details Error");
            }
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
                UserDialogs.Instance.ShowLoading("Logging In...");
                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                var userLogin = await this.apiService.LogUserIn(new UserLoginModel()
                {
                    EmailAddress = this.Username,
                    Password = this.Password
                }).ConfigureAwait(false);

                UserDialogs.Instance.ShowLoading("Loading...");
                
                if (userLogin != null)
                {
                    if (!String.IsNullOrWhiteSpace(userLogin.Token))
                    {
                        SettingsService.UserToken = userLogin.Token;
                        SettingsService.Username = userLogin.Username;
                        SettingsService.Password = this.Password;

                        SettingsService.OwnerId = userLogin.OwnerId.ToString();

                        UserDialogs.Instance.Toast($"Successfully logged in {userLogin.Username}");

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Current.MainPage = Shell.Current ?? new AppShell();
                        });
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("No user found with the given username and password.", "Log In Failed");
                    }
                }
                else if (!String.IsNullOrWhiteSpace(SettingsService.UserToken) && 
                         !String.IsNullOrWhiteSpace(SettingsService.Username) && 
                         !String.IsNullOrWhiteSpace(SettingsService.Password))
                {
                    if (await UserDialogs.Instance.ConfirmAsync("Could not log you into the server.\n Would you like to try and log in locally?",
                                                                "", "Yes", "No").ConfigureAwait(false))
                    {
                        if ((this.Username == SettingsService.Username) && (this.Password == SettingsService.Password))
                        {
                            await UserDialogs.Instance.AlertAsync("You been successfully logged in locally.\n" +
                                "Please log out and re-establish connection to the internet when possible.").ConfigureAwait(false);

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                App.Current.MainPage = Shell.Current ?? new AppShell();
                            });
                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync("User details don't match the locally stored details.").ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Please re-establish connection and try to log in again.");
                    }
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Unable to connect to the server to log you in.\nPlease check your connection and try again.", "Log In Failed");
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.Message, "Log In Error").ConfigureAwait(false);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        void RefreshCanExecutes()
        {
            (this.LogoutCommand as Command).ChangeCanExecute();
            (this.LoginCommand as Command).ChangeCanExecute();
            (this.RegisterCommand as Command).ChangeCanExecute();
            (this.ForgotPasswordCommand as Command).ChangeCanExecute();
        }

        #endregion

        #region Instance Fields

        private string username;

        private string password;

        private bool hidePassword;

        private IServiceCommunication apiService;

        #endregion
    }
}
