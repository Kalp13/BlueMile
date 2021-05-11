using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class OwnersViewModel : BaseViewModel
    {
        #region Instance Properties

        public ObservableCollection<OwnerMobileModel> Owners
        {
            get { return this.owners; }
            set
            {
                if (this.owners != value)
                {
                    this.owners = value;
                    this.OnPropertyChanged(nameof(this.Owners));
                }
            }
        }

        public OwnerMobileModel SelectedOwner
        {
            get { return this.selectedOwner; }
            set
            {
                if (this.selectedOwner != value)
                {
                    this.selectedOwner = value;
                    this.OnPropertyChanged(nameof(this.SelectedOwner));
                }
            }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    this.OnPropertyChanged(nameof(this.IsRefreshing));
                }
            }
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }

        public ICommand AddOwnerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public OwnersViewModel()
        {
            this.InitCommands();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            
        }

        #endregion

        #region Instance Fields

        private ObservableCollection<OwnerMobileModel> owners;

        private OwnerMobileModel selectedOwner;
        private bool isRefreshing;

        #endregion
    }
}
