using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {

        private DocumentMobileModel profileImage;

        public DocumentMobileModel ProfileImage
        {
            get { return this.profileImage; }
            set
            {
                if (this.profileImage != value)
                {
                    this.profileImage = value;
                    this.OnPropertyChanged(nameof(this.ProfileImage));
                }
            }
        }

        public ICommand ChangePhotoCommand
        {
            get;
            private set;
        }

        public ICommand LogOutCommand
        {
            get;
            private set;
        }

        public ICommand SettingsCommand
        {
            get;
            private set;
        }

        public ICommand BoatsCommand
        {
            get;
            private set;
        }

        public AppShellViewModel()
        {
            this.InitCommands();
        }

        private void InitCommands()
        {
            this.ChangePhotoCommand = new Command(async () =>
            {
                this.ProfileImage = await CapturePhotoService.CapturePhotoAsync("OwnerPhoto").ConfigureAwait(false);
            });
            this.LogOutCommand = new Command(() =>
            {
                UserDialogs.Instance.ShowLoading("Logging Out...");
                SettingsService.OwnerId = String.Empty;
                SettingsService.Username = String.Empty;
                SettingsService.Password = String.Empty;
                App.Current.MainPage = new LoginPage();
                UserDialogs.Instance.HideLoading();
            });
            this.SettingsCommand = new Command(async () =>
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.settingsRoute}", true).ConfigureAwait(false);
            });
            this.BoatsCommand = new Command(async () =>
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{Constants.boatsRoute}", true).ConfigureAwait(false);
            });
        }
    }
}
