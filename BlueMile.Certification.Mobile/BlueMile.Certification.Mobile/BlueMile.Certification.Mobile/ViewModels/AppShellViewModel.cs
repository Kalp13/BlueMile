using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {

        private ImageModel profileImage;

        public ImageModel ProfileImage
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

        public AppShellViewModel()
        {
            this.ChangePhotoCommand = new Command(async () =>
            {
                this.ProfileImage = await CapturePhotoService.CapturePhotoAsync("OwnerPhoto").ConfigureAwait(false);
            });
        }
    }
}
