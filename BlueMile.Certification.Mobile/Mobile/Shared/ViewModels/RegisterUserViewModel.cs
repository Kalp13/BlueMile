using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
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

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged(nameof(this.FirstName));
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
                    Name = this.FirstName,
                    Surname = this.LastName,
                };
                var register = await App.ApiService.RegisterUser(registration, owner).ConfigureAwait(false);

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

        #endregion
    }
}
