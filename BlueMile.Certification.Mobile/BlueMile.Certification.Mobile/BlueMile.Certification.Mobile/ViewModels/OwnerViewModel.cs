﻿using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class OwnerViewModel : BaseViewModel
    {
        #region Instance Properties

        public OwnerMobileModel CurrentOwner
        {
            get { return this.currentOwner; }
            set
            {
                if (this.currentOwner != value)
                {
                    this.currentOwner = value;
                    base.OnPropertyChanged(nameof(this.CurrentOwner));
                }
            }
        }

        public List<ImageMobileModel> OwnerImages
        {
            get { return this.ownerImages; }
            set
            {
                if (this.ownerImages != value)
                {
                    this.ownerImages = value;
                    this.OnPropertyChanged(nameof(this.OwnerImages));
                }
            }
        }

        public ICommand EditOwnerCommand
        {
            get;
            private set;
        }

        public ICommand SyncCommand
        {
            get;
            private set;
        }

        public ImageSource MenuImage
        {
            get { return this.menuImage; }
            set
            {
                if (this.menuImage != value)
                {
                    this.menuImage = value;
                    this.OnPropertyChanged(nameof(this.MenuImage));
                }
            }
        }

        #endregion

        #region Constructor

        public OwnerViewModel()
        {
            this.InitCommands();
            
            this.GetOwner().ConfigureAwait(false);
        }

        #endregion

        #region Class Methods

        public void InitCommands()
        {
            this.SyncCommand = new Command(async() =>
            {

            });
            this.EditOwnerCommand = new Command(async () =>
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                await Shell.Current.Navigation.PushAsync(new CreateUpdateOwnerPage()).ConfigureAwait(false);
                UserDialogs.Instance.HideLoading();
            });
        }

        private async Task GetOwner()
        {
            this.CurrentOwner = (await App.DataService.FindAllOwners().ConfigureAwait(false)).FirstOrDefault();
            App.OwnerId = this.CurrentOwner.Id;
            SettingsService.OwnerId = this.CurrentOwner.Id.ToString();
            this.OwnerImages = new List<ImageMobileModel>();
            this.OwnerImages.Add(this.CurrentOwner.IcasaPopPhoto);
            this.OwnerImages.Add(this.CurrentOwner.IdentificationDocument);
            this.OwnerImages.Add(this.CurrentOwner.SkippersLicenseImage);

            if (this.CurrentOwner != null)
            {
                this.Title = String.Format(CultureInfo.InvariantCulture, "{0}'s Details", this.CurrentOwner.Name);
                this.MenuImage = ImageSource.FromFile("edit.png");
            }
            else
            {
                this.Title = "No Owner Available";
                this.MenuImage = ImageSource.FromFile("add.png");
            }
        }

        #endregion

        #region Instance Fields

        private OwnerMobileModel currentOwner;

        private List<ImageMobileModel> ownerImages;

        private ImageSource menuImage;

        #endregion
    }
}
