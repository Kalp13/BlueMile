using BlueMile.Certification.Mobile.Data;
using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public interface IDataService
    {
        #region Owner Data Methods

        Task<bool> CreateNewOwnerAsync(OwnerMobileModel owner);

        Task<OwnerMobileModel> FindOwnerBySystemIdAsync(Guid systemId);

        Task<OwnerMobileModel> FindOwnerByIdAsync(long id);

        Task<bool> UpdateOwnerAsync(OwnerMobileModel owner);

        Task<List<OwnerMobileModel>> FindOwnersAsync();

        #endregion

        #region Boat Data Methods

        Task<bool> CreateNewBoatAsync(BoatMobileModel owner);

        Task<BoatMobileModel> FindBoatBySystemIdAsync(Guid systemId);

        Task<BoatMobileModel> FindBoatByIdAsync(long id);

        Task<bool> UpdateBoatAsync(BoatMobileModel owner);

        Task<List<BoatMobileModel>> GetBoatsByOwnerIdAsync();

        #endregion

        #region Item Data Methods

        Task<bool> CreateNewItemAsync(ItemMobileModel owner);

        Task<ItemMobileModel> FindItemBySystemIdAsync(Guid systemId);

        Task<ItemMobileModel> FindItemByIdAsync(long id);

        Task<bool> UpdateItemAsync(ItemMobileModel owner);

        Task<List<ItemMobileModel>> GetItemsByBoatAsync(long boatId);

        #endregion
    }
}
