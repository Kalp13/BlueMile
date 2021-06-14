using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Services
{
    /// <summary>
    /// <c>ICertificationRepository</c> contains all of the relevant 
    /// CRUD operations for <c>Owners</c>, <c>Boats</c> and <c>Items</c>.
    /// </summary>
    public interface ICertificationRepository
    {
        #region Owner Methods

        /// <summary>
        /// Finds all of the owners with the given filtering criteria.
        /// </summary>
        /// <param name="findOwnerModel">
        ///     The filtering criteria to find the owners with.
        /// </param>
        /// <returns>
        ///     Returns a list of <see cref="OwnerModel"/> matching the filtering criteria.
        /// </returns>
        Task<List<OwnerModel>> FindAllOwners(FindOwnerModel findOwnerModel);

        /// <summary>
        /// Finds an <see cref="OwnerModel"/> with the given unique identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="OwnerModel"/> containing all of the owner details.
        /// </returns>
        Task<OwnerModel> FindOwnerById(Guid ownerId);

        /// <summary>
        /// Finds an <see cref="OwnerModel"/> with the given username.
        /// </summary>
        /// <param name="username">
        ///     The username of the owner to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="OwnerModel"/> with the given username.
        /// </returns>
        Task<OwnerModel> FindOwnerByUsername(string username);

        /// <summary>
        /// Determines whether a user with the specified unique identifier exists.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner to validate.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the owner exists.
        /// </returns>
        Task<bool> DoesOwnerExist(Guid ownerId);

        /// <summary>
        /// Creates a new <c>IndividualOwner</c> from the given <see cref="CreateOwnerModel"/>.
        /// </summary>
        /// <param name="entity">
        ///     Contains all of the relevant properties of the owner that will be created.
        /// </param>
        /// <returns>
        ///     Returns the unique identifier of the owner created.
        /// </returns>
        Task<Guid> CreateOwner(CreateOwnerModel entity);

        /// <summary>
        /// Updates an existing owner with the given <see cref="UpdateOwnerModel"/> properties.
        /// </summary>
        /// <param name="entity">
        ///     The updated details of the owner to update.
        /// </param>
        /// <returns>
        ///     Returns the unique identifier of the owner that was updated.
        /// </returns>
        Task<Guid> UpdateOwner(UpdateOwnerModel entity);

        /// <summary>
        /// Deletes the owner with the given unique identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner to delete.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the owner was successfully removed.
        /// </returns>
        Task<bool> DeleteOwner(Guid ownerId);

        #endregion

        #region Boat Methods

        /// <summary>
        /// Finds all of the boats with the given filtering parameters.
        /// </summary>
        /// <param name="model">
        ///     The filtering parameters to find the boats with.
        /// </param>
        /// <returns>
        ///     Returns a list of <see cref="BoatModel"/> of all the boats that match the filtering parameters.
        /// </returns>
        Task<List<BoatModel>> FindAllBoats(FindBoatsModel model);

        /// <summary>
        /// Finds all of the <see cref="BoatModel"/> linked to the given unique owner identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner linked to the boats.
        /// </param>
        /// <returns>
        ///     Returns a list <see cref="BoatModel"/> linked to the given owner.
        /// </returns>
        Task<List<BoatModel>> FindAllBoatsByOwnerId(Guid ownerId);

        /// <summary>
        /// Finds a specific boat with the given unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="BoatModel"/> matching the given unique identifier.
        /// </returns>
        Task<BoatModel> FindBoatById(Guid boatId);

        /// <summary>
        /// Determines whether a boat with the given unique identifier exists.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat to validate.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the boat exists.
        /// </returns>
        Task<bool> DoesBoatExist(Guid boatId);

        /// <summary>
        /// Creates a new <c>Boat</c> with the given <see cref="CreateBoatModel"/>.
        /// </summary>
        /// <param name="entity">
        ///     The <see cref="CreateBoatModel"/> containing all of the properties to create the boat with.
        /// </param>
        /// <returns>
        ///     Returns a unique identifier of the boat that was created.
        /// </returns>
        Task<Guid> CreateBoat(CreateBoatModel entity);

        /// <summary>
        /// Updates an existing boat with the given <see cref="UpdateBoatModel"/>.
        /// </summary>
        /// <param name="entity">
        ///     The updated properties of the boat.
        /// </param>
        /// <returns>
        ///     Returns a unique identifier of the updated boat.
        /// </returns>
        Task<Guid> UpdateBoat(UpdateBoatModel entity);

        /// <summary>
        /// Deletes an existing boat with the given unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the boat was removed successfully.
        /// </returns>
        Task<bool> DeleteBoat(Guid boatId);

        #endregion

        #region Item Methods

        /// <summary>
        /// Finds all of the items matching the given filtering parameters.
        /// </summary>
        /// <param name="model">
        ///     The filtering parameters to find the items with.
        /// </param>
        /// <returns>
        ///     Returns a list of <see cref="ItemModel"/> matching the given filtering parameters.
        /// </returns>
        Task<List<ItemModel>> FindAllItems(FindItemsModel model);

        /// <summary>
        /// Finds a list of <see cref="ItemModel"/> linked to the given boat unique identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat.
        /// </param>
        /// <returns>
        ///     Returns a list <see cref="ItemModel"/>.
        /// </returns>
        Task<List<ItemModel>> FindItemsByBoatId(Guid boatId);

        /// <summary>
        /// Finds a specific <see cref="ItemModel"/> with the given unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the item.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="ItemModel"/> matching the given unique identifier.
        /// </returns>
        Task<ItemModel> FindItemById(Guid id);

        /// <summary>
        /// Determines whether a specified <c>Item</c> exists.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the item to validate.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if an item exists.
        /// </returns>
        Task<bool> DoesItemExist(Guid id);

        /// <summary>
        /// Creates a new <c>Item</c> with the given <see cref="CreateItemModel"/> properties.
        /// </summary>
        /// <param name="entity">
        ///     The properties of the item to create.
        /// </param>
        /// <returns>
        ///     The unique identifier of the item created.
        /// </returns>
        Task<Guid> CreateItem(CreateItemModel entity);

        /// <summary>
        /// Updates an existing <c>Item</c> with the given <see cref="UpdateItemModel"/> properties.
        /// </summary>
        /// <param name="entity">
        ///     The updated item properties.
        /// </param>
        /// <returns>
        ///     The unique identifier of the updated item.
        /// </returns>
        Task<Guid> UpdateItem(UpdateItemModel entity);

        /// <summary>
        /// Deletes an existing <c>Item</c> with the given unique identifier.
        /// </summary>
        /// <param name="itemId">
        ///     The unique identifier of the item.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the item was successfully removed.
        /// </returns>
        Task<bool> DeleteItem(Guid itemId);

        #endregion
    }
}
