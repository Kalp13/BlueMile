using BlueMile.Certification.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
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

        #endregion

        #region Constructor

        public RegisterUserViewModel()
        {
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
                await Shell.Current.Navigation.PushAsync(new RegisterUserPage());
            });
        }

        #endregion

        #region Instance Fields

        private string username;

        private string password;

        private bool hidePassword;

        #endregion
    }
}
