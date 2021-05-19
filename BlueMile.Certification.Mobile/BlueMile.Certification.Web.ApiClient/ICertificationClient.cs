using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Web.ApiClient
{
    public interface ICertificationApiClient
    {
        #region User Methods

        /// <summary>
        /// Registers a new user with the given <see cref="UserRegistrationModel"/>
        /// and <see cref="OwnerModel"/> data.
        /// </summary>
        /// <param name="userModel">
        ///     The user registration details to register the user with.
        /// </param>
        /// <param name="ownerModel">
        ///     The owner details to use and link to the user that will be registered.
        /// </param>
        /// <returns></returns>
        Task<bool> RegisterUser(UserRegistrationModel userModel);

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

        Task<OwnerModel> GetOwnerBySystemId(Guid ownerId);

        Task<OwnerModel> GetOwnerByUsername(string username);

        Task<Guid> CreateOwner(OwnerModel owner);

        Task<Guid> UpdateOwner(OwnerModel owner);

        #endregion

        #region Boat Methods

        Task<List<BoatModel>> GetBoatsByOwnerId(Guid ownerId);

        Task<BoatModel> GetBoatById(Guid boatId);

        Task<Guid> CreateBoat(BoatModel boat);

        Task<Guid> UpdateBoat(BoatModel boat);

        #endregion

        #region Item Methods

        Task<List<ItemModel>> GetBoatRequiredItems(Guid boatId);

        Task<Guid> CreateItem(ItemModel item);

        Task<Guid> UpdateItem(ItemModel item);

        #endregion
    }
}
