using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Coc.Mobile.Services
{
    public interface ISqlDataService
    {
        #region Owner Methods

        /// <summary>
        /// Gets all of the owners stored locally.
        /// </summary>
        /// <returns>
        ///     Returns a list of <see cref="OwnerModel"/>'s mapped from stored data.
        /// </returns>
        Task<List<OwnerModel>> GetAllOwners();

        /// <summary>
        /// Creates a new owner in the local database.
        /// </summary>
        /// <param name="newOwner">
        ///     The new <see cref="OwnerModel"/> to map <see cref="OwnerEntity"/> to store.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating whether the new record 
        ///     was successfully created and stored.
        /// </returns>
        Task<bool> CreateNewOwner(OwnerModel newOwner);

        /// <summary>
        /// Gets the specific owner with the unique identifier specified.
        /// </summary>
        /// <param name="ownerId">
        ///     The idnetifier to retrieve a specific <see cref="OwnerModel"/> with.
        /// </param>
        /// <returns></returns>
        Task<OwnerModel> GetOwnerById(Guid ownerId);

        /// <summary>
        /// Updates the locally stored owner with the matching unique identifier and
        /// with the properties supplied.
        /// </summary>
        /// <param name="updatedOwner">
        ///     The <see cref="OwnerModel"/> to use to update the <see cref="OwnerEntity"/> record with.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the owner's properties were successfully updated.
        /// </returns>
        Task<bool> UpdateOwner(OwnerModel updatedOwner);

        #endregion

        #region Boat Methods

        /// <summary>
        /// Creates a new boat in the local database.
        /// </summary>
        /// <param name="newBoat">
        ///     The new boat to create.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the new boat was successfully created.
        /// </returns>
        Task<bool> CreateNewBoat(BoatModel newBoat);

        /// <summary>
        /// Gets all the <see cref="BoatModel"/> linked to the given owner identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The owner's id to retrieve the boats with.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="List{BoatModel}"/> linked to the given owner identifier.
        /// </returns>
        Task<List<BoatModel>> GetAllBoats(Guid ownerId);

        /// <summary>
        /// Gets <see cref="BoatModel"/> with the given boat identifier and owner identifier.
        /// </summary>
        /// <param name="boatId">
        ///     The unique boat identifier to find the boat with.
        /// </param>
        /// <param name="ownerId">
        ///     The unique owner identifier to use with the boat identifier to find the specific boat with.
        /// </param>
        /// <returns>
        ///     Returns the specific <see cref="BoatModel"/> that matches the corresponsing properties.
        /// </returns>
        Task<BoatModel> GetBoatById(Guid boatId);

        /// <summary>
        /// Updates the locally stored boat with the matching unique identifier and
        /// with the properties supplied.
        /// </summary>
        /// <param name="updatedBoat">
        ///     The <see cref="BoatModel"/> to use to update the <see cref="BoatEntity"/> record with.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the boat was successfully updated.
        /// </returns>
        Task<bool> UpdateBoat(BoatModel updatedBoat);

        #endregion

        #region Required Item Methods

        /// <summary>
        /// Inserts a new <see cref="RequiredItemEntity"/> that is mapped from the given <see cref="RequiredItemModel"/> into the local database.
        /// </summary>
        /// <param name="requiredItem">
        ///     The <see cref="RequiredItemModel"/> to map to a <see cref="RequiredItemEntity"/> and save in the local database.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the <see cref="RequiredItemModel"/> was successfully mapepd and saved.
        /// </returns>
        Task<bool> CreateNewItem(RequiredItemModel requiredItem);

        /// <summary>
        /// Updates a <see cref="RequiredItemEntity"/> item with the given <see cref="RequiredItemModel"/>.
        /// </summary>
        /// <param name="requiredItem">
        ///     The <see cref="RequiredItemModel"/> to update in the local database.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the record was updated successfully.
        /// </returns>
        Task<bool> UpdateRequireditem(RequiredItemModel requiredItem);

        /// <summary>
        /// Gets all of the <see cref="RequiredItemModel"/> mapped from the stored <see cref="RequiredItemEntity"/>.
        /// </summary>
        /// <param name="boatId">
        ///     The unique boat identifier to retrieve the <see cref="RequiredItemEntity"/> with.
        /// </param>
        /// <returns>
        ///     Returns a collection all teh <see cref="RequiredItemModel"/>'s found with the given baot identifier.
        /// </returns>
        Task<List<RequiredItemModel>> GetItemsByBoatId(Guid boatId);

        /// <summary>
        /// Gets a specific Required item with the unique identifier specified.
        /// </summary>
        /// <param name="itemId">
        ///     The unique identifier for the item to retrieve.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="RequiredItemModel"/> that is mapped 
        ///     from the <see cref="RequiredItemEntity"/> retrieved from the local database.
        /// </returns>
        Task<RequiredItemModel> GetItemById(Guid itemId);

        #endregion

        #region Image Methods

        /// <summary>
        /// Creates a new <see cref="ImageEntity"/> from the given <see cref="ImageModel"/>.
        /// </summary>
        /// <param name="image">
        ///     The image item to map and save.
        /// </param>
        /// <returns>
        ///     Returns the saved <see cref="ImageEntity"/> that is stored.
        /// </returns>
        Task<bool> CreateNewImage(ImageModel image);

        /// <summary>
        /// Updates an <see cref="ImageEntity"/> from the given <see cref="ImageModel"/>.
        /// </summary>
        /// <param name="image">
        ///     The <see cref="ImageModel"/>
        /// </param>
        /// <returns></returns>
        Task<bool> UpdateImage(ImageModel image);

        /// <summary>
        /// Gets an image with the given unique identifier.
        /// </summary>
        /// <param name="imageId">
        ///     The unique identifier of the image to retrieve.
        /// </param>
        /// <returns>
        ///     The image entity retrieved.
        /// </returns>
        Task<ImageEntity> GetImageById(Guid imageId);

        #endregion
    }
}
