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

        Task<Guid> CreateNewOwnerAsync(OwnerMobileModel owner);

        Task<OwnerMobileModel> FindOwnerBySystemIdAsync(Guid systemId);

        Task<OwnerMobileModel> FindOwnerByIdAsync(Guid id);

        Task<bool> UpdateOwnerAsync(OwnerMobileModel owner);

        Task<List<OwnerMobileModel>> FindOwnersAsync();

        #endregion

        #region Boat Data Methods

        Task<List<BoatMobileModel>> FindBoatsByOwnerIdAsync(Guid ownerId);

        Task<Guid> CreateNewBoatAsync(BoatMobileModel owner);

        Task<BoatMobileModel> FindBoatBySystemIdAsync(Guid systemId);

        Task<BoatMobileModel> FindBoatByIdAsync(Guid id);

        Task<bool> UpdateBoatAsync(BoatMobileModel owner);

        #endregion

        #region Item Data Methods

        Task<Guid> CreateNewItemAsync(ItemMobileModel owner);

        Task<ItemMobileModel> FindItemBySystemIdAsync(Guid systemId);

        Task<ItemMobileModel> FindItemByIdAsync(Guid id);

        Task<bool> UpdateItemAsync(ItemMobileModel owner);

        Task<List<ItemMobileModel>> FindItemsByBoatIdAsync(Guid boatId);

        #endregion

        #region Image Methods

        Task<Guid> CreateNewImageAsync(DocumentMobileModel image);

        Task<bool> UpdateImageAsync(DocumentMobileModel image);

        Task<DocumentMobileModel> FindImageByIdAsync(Guid imageId);

        #endregion
    }
}
