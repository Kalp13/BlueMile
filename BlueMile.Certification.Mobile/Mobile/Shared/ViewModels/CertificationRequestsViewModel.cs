using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class CertificationRequestsViewModel : BaseViewModel
    {
        #region Instance Properties

        public ObservableCollection<CertificationRequestMobileModel> Requests
        {
            get { return this.requests; }
            set
            {
                if (this.requests != value)
                {
                    this.requests = value;
                    this.OnPropertyChanged(nameof(this.Requests));
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

        #endregion

        #region Constructor

        public CertificationRequestsViewModel()
        {
            this.InitCommands();
            this.GetCertificationRequestsAsync().ConfigureAwait(false);
        }

        #endregion

        #region Instance Methods

        public void InitCommands()
        {

        }

        private async Task GetCertificationRequestsAsync()
        {
            try
            {
                
            }
            catch (Exception exc)
            {
                Crashes.TrackError(exc);
                await UserDialogs.Instance.AlertAsync("Could not retrieve certification requests: " + exc.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

        #region Instance Fields

        private ObservableCollection<CertificationRequestMobileModel> requests;

        private bool isRefreshing;

        private IServiceCommunication apiService;

        #endregion
    }
}
