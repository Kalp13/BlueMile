using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueMile.Certification.Web.ApiClient
{
    /// <summary>
    /// <c>ICertificationApiClient</c> contains all of the relevant 
    /// CRUD operations for <c>Owners</c>, <c>Boats</c> and <c>Items</c>.
    /// </summary>
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

        /// <summary>
        /// Determines whehter or not the service is active.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsServiceActive();

        #endregion

        #region Owner Methods

        /// <summary>
        /// Finds all of the owners with the given filtering criteria.
        /// </summary>
        /// <param name="model">
        ///     The filtering criteria to find the owners with.
        /// </param>
        /// <returns>
        ///     Returns a list of <see cref="OwnerModel"/> matching the filtering criteria.
        /// </returns>
        Task<PagedResponseModel<OwnerModel>> FindOwners(FindOwnerModel model);

        /// <summary>
        /// Finds an <see cref="OwnerModel"/> with the given unique identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="OwnerModel"/> containing all of the owner details.
        /// </returns>
        Task<OwnerModel> GetOwnerBySystemId(Guid ownerId);

        /// <summary>
        /// Finds an <see cref="OwnerModel"/> with the given username.
        /// </summary>
        /// <param name="username">
        ///     The username of the owner to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="OwnerModel"/> with the given username.
        /// </returns>
        Task<OwnerModel> GetOwnerByUsername(string username);

        /// <summary>
        /// Creates a new <see cref="Owner"/> from the given <see cref="OwnerModel"/>.
        /// </summary>
        /// <param name="entity">
        ///     Contains all of the relevant properties of the owner that will be created.
        /// </param>
        /// <returns>
        ///     Returns the unique identifier of the owner created.
        /// </returns>
        Task<Guid> CreateOwner(OwnerModel owner);

        /// <summary>
        /// Updates an existing owner with the given <see cref="OwnerModel"/> properties.
        /// </summary>
        /// <param name="entity">
        ///     The updated details of the owner to update.
        /// </param>
        /// <returns>
        ///     Returns the unique identifier of the owner that was updated.
        /// </returns>
        Task<Guid> UpdateOwner(OwnerModel owner);

        #endregion

        #region Boat Methods

        /// <summary>
        /// Finds all of the <see cref="BoatModel"/> linked to the given unique owner identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner linked to the boats.
        /// </param>
        /// <returns>
        ///     Returns a list <see cref="BoatModel"/> linked to the given owner.
        /// </returns>
        Task<List<BoatModel>> GetBoatsByOwnerId(Guid ownerId);

        /// <summary>
        /// Finds a specific boat with the given unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="BoatModel"/> matching the given unique identifier.
        /// </returns>
        Task<BoatModel> GetBoatById(Guid boatId);

        /// <summary>
        /// Creates a new <c>Boat</c> with the given <see cref="BoatModel"/>.
        /// </summary>
        /// <param name="boat">
        ///     The <see cref="BoatModel"/> containing all of the properties to create the boat with.
        /// </param>
        /// <returns>
        ///     Returns a unique identifier of the boat that was created.
        /// </returns>
        Task<Guid> CreateBoat(BoatModel boat);

        /// <summary>
        /// Updates an existing boat with the given <see cref="BoatModel"/>.
        /// </summary>
        /// <param name="boat">
        ///     The updated properties of the boat.
        /// </param>
        /// <returns>
        ///     Returns a unique identifier of the updated boat.
        /// </returns>
        Task<Guid> UpdateBoat(BoatModel boat);

        /// <summary>
        /// Determines whether or not a certain boat exists with the given unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat to validate.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the boat exists or not.
        /// </returns>
        Task<bool> DoesBoatExist(Guid boatId);

        #endregion

        #region Item Methods

        /// <summary>
        /// Finds a list of <see cref="ItemModel"/> linked to the given boat unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat.
        /// </param>
        /// <returns>
        ///     Returns a list <see cref="ItemModel"/>.
        /// </returns>
        Task<List<ItemModel>> GetBoatRequiredItems(Guid boatId);

        /// <summary>
        /// Finds a specific <see cref="ItemModel"/> with the given unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the item.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="ItemModel"/> matching the given unique identifier.
        /// </returns>
        Task<ItemModel> GetItemById(Guid itemId);

        /// <summary>
        /// Creates a new <c>Item</c> with the given <see cref="ItemModel"/> properties.
        /// </summary>
        /// <param name="item">
        ///     The properties of the item to create.
        /// </param>
        /// <returns>
        ///     The unique identifier of the item created.
        /// </returns>
        Task<Guid> CreateItem(ItemModel item);

        /// <summary>
        /// Updates an existing <c>Item</c> with the given <see cref="ItemModel"/> properties.
        /// </summary>
        /// <param name="item">
        ///     The updated item properties.
        /// </param>
        /// <returns>
        ///     The unique identifier of the updated item.
        /// </returns>
        Task<Guid> UpdateItem(ItemModel item);

        /// <summary>
        /// Determines whether or not a certain item exists with the given unique identifier.
        /// </summary>
        /// <param name="itemId">
        ///     The unique identifier of the item to validate.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the item exists or not.
        /// </returns>
        Task<bool> DoesItemExist(Guid itemId);

        #endregion

        #region Certification Methods

        /// <summary>
        /// Creates a new <c>CertificationRequest</c> from the given <see cref="CertificationRequestModel"/>.
        /// </summary>
        /// <param name="model">
        ///     The <see cref="CertificationRequestModel"/> containing all the properties for the new request.
        /// </param>
        /// <returns>
        ///     Returns a unique identifier of the request created.
        /// </returns>
        Task<Guid> CreateCertificationRequest(CertificationRequestModel model);

        /// <summary>
        /// Finds a list of certification requests matching the filtering parameters given.
        /// </summary>
        /// <param name="model">
        ///     The filtering criteria to find the <see cref="CertificationRequest"/>s with.
        /// </param>
        /// <returns>
        ///     Returns the collection of <see cref="CertificationRequestModel"/> found.
        /// </returns>
        Task<CertificationRequestModel[]> FindCertificationRequests(FindCertificationRequestsModel model);

        #endregion
    }
}
