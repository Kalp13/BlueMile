using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.ExternalServices
{
    public interface IServiceCommunication
    {
        #region Security Methods

        /// <summary>
        /// Registers a new user with the given <see cref="UserRegistrationModel"/>
        /// and <see cref="OwnerMobileModel"/> data.
        /// </summary>
        /// <param name="userModel">
        ///     The user registration details to register the user with.
        /// </param>
        /// <param name="ownerModel">
        ///     The owner details to use and link to the user that will be registered.
        /// </param>
        /// <returns></returns>
        Task<bool> RegisterUser(UserRegistrationModel userModel, OwnerMobileModel ownerModel);

        /// <summary>
        /// Logs a user in with the given <see cref="UserLoginModel"/> details.
        /// </summary>
        /// <param name="userModel">
        ///     The user details to log in with.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="UserToken"/> containing the user's unique authentication token and properties.
        /// </returns>
        Task<UserToken> LogUserIn(UserLoginModel userModel);

        #endregion

        #region Owner Methods

        Task<OwnerMobileModel> GetOwnerBySystemId(Guid ownerId);

        Task<OwnerMobileModel> GetOwnerByUsername(string username);

        Task<Guid> CreateOwner(CreateOwnerModel owner);

        Task<Guid> UpdateOwner(UpdateOwnerModel owner);

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
