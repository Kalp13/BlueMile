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

        /// <summary>
        /// Gets an owner from the server with the given unique identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner on the server.
        /// </param>
        /// <returns>
        ///     Returns an <see cref="OwnerMobileModel"/> with the given unique identifier.
        /// </returns>
        Task<OwnerMobileModel> GetOwnerBySystemId(Guid ownerId);

        /// <summary>
        /// Gets an owner from the server with the given username.
        /// </summary>
        /// <param name="username">
        ///     The username of the owner to find.
        /// </param>
        /// <returns>
        ///     Returns an <see cref="OwnerMobileModel"/> matching the given username.
        /// </returns>
        Task<OwnerMobileModel> GetOwnerByUsername(string username);

        /// <summary>
        /// Creates an owner from the given properties and returns the owners unique identifier on the system.
        /// </summary>
        /// <param name="owner">
        ///     All of the owner details required for creating an owner.
        /// </param>
        /// <returns>
        ///     Returns the unique identifier on the server of the owner created.
        /// </returns>
        Task<Guid> CreateOwner(OwnerMobileModel owner);

        /// <summary>
        /// Updates an existing owner on the system with the properties provided.
        /// </summary>
        /// <param name="owner">
        ///     The unique identifier of the owner on the server.
        /// </param>
        /// <returns>
        ///     Returns the unique identifier of the owner updated.
        /// </returns>
        Task<Guid> UpdateOwner(OwnerMobileModel owner);

        #endregion

        #region Boat Methods

        /// <summary>
        /// Gets a list of boats with the given unique owner identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="List{BoatMobileModel}"/> found for the given owner.
        /// </returns>
        Task<List<BoatMobileModel>> GetBoatsByOwnerId(Guid ownerId);
        
        /// <summary>
        /// Gets a boat with the given unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unqiue system identifier of the boat.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="BoatMobileModel"/> matching the unique identifier of the boat found.
        /// </returns>
        Task<BoatMobileModel> GetBoatById(Guid boatId);

        /// <summary>
        /// Creates a new boat with the given <see cref="BoatMobileModel"/> properties.
        /// </summary>
        /// <param name="boat">
        ///     The <see cref="BoatMobileModel"/> containing all of the boat properties.
        /// </param>
        /// <returns>
        ///     Returns the unique system identifier of the boat created.
        /// </returns>
        Task<Guid> CreateBoat(BoatMobileModel boat);

        /// <summary>
        /// Updates an existing boat on the server with the given properties.
        /// </summary>
        /// <param name="boat">
        ///     The properties of the boat to update.
        /// </param>
        /// <returns>
        ///     Returns the unique server identifier of the boat updated.
        /// </returns>
        Task<Guid> UpdateBoat(BoatMobileModel boat);

        #endregion

        #region Item Methods

        /// <summary>
        /// Gets a <see cref="List{ItemMobileModel}"/> containing all of the items 
        /// linked to the given unique identifier of the boat.
        /// </summary>
        /// <param name="boatId">
        ///     The unique system identifier of the boat.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="List{ItemMobileModel}"/> containing all of the linked items found.
        /// </returns>
        Task<List<ItemMobileModel>> GetBoatRequiredItems(Guid boatId);

        /// <summary>
        /// Creates a new item from the given <see cref="ItemMobileModel"/>.
        /// </summary>
        /// <param name="item">
        ///     The item's properties to create.
        /// </param>
        /// <returns>
        ///     Returns the server unique identifier of the item created.
        /// </returns>
        Task<Guid> CreateItem(ItemMobileModel item);

        /// <summary>
        /// Updates an existing item with the given <see cref="ItemMobileModel"/>.
        /// </summary>
        /// <param name="item">
        ///     The item properties to update with.
        /// </param>
        /// <returns>
        ///     Returns the unqiue system identifier of the updated item.
        /// </returns>
        Task<Guid> UpdateItem(ItemMobileModel item);

        /// <summary>
        /// Retrieves an item with the given unique identifier.
        /// </summary>
        /// <param name="itemId">
        ///     The unique identifier of the item.
        /// </param>
        /// <returns>
        ///     Returns an <see cref="ItemMobileModel"/> with the item details.
        /// </returns>
        Task<ItemMobileModel> GetItemById(Guid itemId);

        #endregion
    }
}
