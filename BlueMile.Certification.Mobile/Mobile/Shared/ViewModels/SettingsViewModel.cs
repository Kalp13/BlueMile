using BlueMile.Certification.Mobile.Services.InternalServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Instance Properties

        public string Username
        {
            get { return SettingsService.Username; }
        }

        public string ServiceAddress
        {
            get { return SettingsService.ServiceAddress; }
        }

        public string OwnerId
        {
            get { return SettingsService.OwnerId; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="SettingsViewModel"/>.
        /// </summary>
        public SettingsViewModel()
        {

        }

        #endregion

        #region Instance Methods



        #endregion

        #region Instance Fields

        #endregion
    }
}
