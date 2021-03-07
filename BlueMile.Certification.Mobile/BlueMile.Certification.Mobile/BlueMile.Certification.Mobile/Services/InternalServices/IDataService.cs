using BlueMile.Certification.Mobile.Data;
using BlueMile.Certification.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public interface IDataService
    {
        #region Owner Data Methods

        Task<bool> CreateNewOwnerAsync(OwnerMobileEntity owner);

        Task<OwnerMobileEntity> FindOwnerBySystemIdAsync(Guid systemId);

        Task<OwnerMobileEntity> FindOwnerByIdAsync(long id);

        Task<bool> UpdateOwnerAsync(OwnerMobileEntity owner);

        #endregion

        #region Boat Data Methods

        Task<bool> CreateNewBoatAsync(BoatMobileEntity owner);

        Task<BoatMobileEntity> FindBoatBySystemIdAsync(Guid systemId);

        Task<BoatMobileEntity> FindBoatByIdAsync(long id);

        Task<bool> UpdateBoatAsync(BoatMobileEntity owner);

        #endregion

        #region Item Data Methods

        Task<bool> CreateNewItemAsync(ItemMobileEntity owner);

        Task<ItemMobileEntity> FindItemBySystemIdAsync(Guid systemId);

        Task<ItemMobileEntity> FindItemByIdAsync(long id);

        Task<bool> UpdateItemAsync(ItemMobileEntity owner);

        #endregion
    }
}
