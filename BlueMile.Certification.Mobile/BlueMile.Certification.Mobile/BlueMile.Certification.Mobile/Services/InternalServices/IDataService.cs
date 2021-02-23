using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public interface IDataService
    {
        #region Owner Data Methods

        Task<bool> CreateNewOwnerAsync();

        Task<Owner>

        Task<bool> UpdateOwnerAsync();

        #endregion

        #region Boat Data Methods

        #endregion

        #region Item Data Methods

        #endregion
    }
}
