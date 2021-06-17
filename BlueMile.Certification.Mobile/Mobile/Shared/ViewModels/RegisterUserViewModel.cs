using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Views;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.AppCenter.Crashes;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class RegisterUserViewModel : BaseViewModel
    {
        #region Instance Properties

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged(nameof(this.FirstName));
                    this.RefreshCanExecutes();
                }
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.OnPropertyChanged(nameof(this.LastName));
                    this.RefreshCanExecutes();
                }
            }
        }

        public string Identification
        {
            get { return this.identification; }
            set
            {
                if (this.identification != value)
                {
                    this.identification = value;
                    this.OnPropertyChanged(nameof(this.Identification));
                    this.RefreshCanExecutes();
                }
            }
        }

        public string ContactNumber
        {
            get { return this.contactNumber; }
            set
            {
                if (this.contactNumber != value)
                {
                    this.contactNumber = value;
                    this.OnPropertyChanged(nameof(this.ContactNumber));
                    this.RefreshCanExecutes();
                }
            }
        }

        public string EmailAddress
        {
            get { return this.emailAddress; }
            set
            {
                if (this.emailAddress != value)
                {
                    this.emailAddress = value;
                    this.OnPropertyChanged(nameof(this.EmailAddress));
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

        public string ConfirmPassword
        {
            get { return this.confirmPassword; }
            set
            {
                if (this.confirmPassword != value)
                {
                    this.confirmPassword = value;
                    this.OnPropertyChanged(nameof(this.ConfirmPassword));
                    this.RefreshCanExecutes();
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
                this.EmailAddress = String.Empty;
                this.FirstName = String.Empty;
                this.LastName = String.Empty;
                this.ContactNumber = String.Empty;
                this.Password = String.Empty;
                this.ConfirmPassword = String.Empty;
                this.Identification = String.Empty;
            });
            this.RegisterCommand = new Command(async () =>
            {
                await this.RegisterUser();
            }, () => (!String.IsNullOrWhiteSpace(this.FirstName) &&
                      !String.IsNullOrWhiteSpace(this.LastName) &&
                      !String.IsNullOrWhiteSpace(this.EmailAddress) &&
                      !String.IsNullOrWhiteSpace(this.ContactNumber) &&
                      !String.IsNullOrWhiteSpace(this.Identification) &&
                      !String.IsNullOrWhiteSpace(this.Password) &&
                      !String.IsNullOrWhiteSpace(this.ConfirmPassword)));
            this.DisplayPasswordCommand = new Command(() =>
            {
                this.HidePassword = !this.HidePassword;
            });
            this.ClearDetailsCommand = new Command(() =>
            {
                
            });
        }

        private async Task RegisterUser()
        {
            try
            {
                var registration = new UserRegistrationModel()
                {
                    EmailAddress = this.EmailAddress,
                    ConfirmPassword = this.ConfirmPassword,
                    Password = this.Password,
                    ContactNumber = this.ContactNumber
                };
                var owner = new OwnerMobileModel()
                {
                    ContactNumber = this.ContactNumber,
                    Email = this.EmailAddress,
                    Identification = this.Identification,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                };

                if (this.apiService == null)
                {
                    this.apiService = new ServiceCommunication();
                }

                bool register = false;
                try
                {
                    register = await this.apiService.RegisterUser(registration, owner);
                }
                catch (WebException webExc)
                {
                    await UserDialogs.Instance.AlertAsync("Could not register user: " + webExc.Message);
                }

                if (register)
                {
                    UserDialogs.Instance.Toast($"Successfully registered with username {registration.EmailAddress}");
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

        void RefreshCanExecutes()
        {
            (this.ClearCommand as Command).ChangeCanExecute();
            (this.ClearDetailsCommand as Command).ChangeCanExecute();
            (this.RegisterCommand as Command).ChangeCanExecute();
            (this.DisplayPasswordCommand as Command).ChangeCanExecute();
        }

        #endregion

        #region Instance Fields

        private bool hidePassword;

        private string firstName;

        private string lastName;

        private string identification;

        private string contactNumber;

        private string emailAddress;

        private string password;

        private string confirmPassword;

        private IServiceCommunication apiService;

        #endregion
    }
}
