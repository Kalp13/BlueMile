using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.ExternalServices
{
    public interface IServiceCommunication
    {
        #region Security Methods

        Task<bool> RegisterUser(UserModel user);

        Task<UserModel> LogUserIn(string username, string password);

        #endregion

        #region Owner Methods

        Task<OwnerMobileModel> GetOwnerByDetails(string username, string password);

        Task<Guid> CreateOwner(OwnerMobileModel owner);

        Task<Guid> UpdateOwner(OwnerMobileModel owner);

        #endregion

        #region Boat Methods

        Task<List<BoatMobileModel>> GetBoatsByOwnerId(Guid ownerId);

        Task<BoatMobileModel> GetBoatById(Guid boatId);

        Task<Guid> CreateBoat(BoatMobileModel boat);

        Task<Guid> UpdateBoat(BoatMobileModel boat);

        #endregion

        #region Item Methods

        Task<List<ItemMobileModel>> GetBoatRequiredItems(Guid boatId);

        Task<Guid> CreateItem(ItemMobileModel item);

        Task<Guid> UpdateItem(ItemMobileModel item);

        #endregion
    }
}
