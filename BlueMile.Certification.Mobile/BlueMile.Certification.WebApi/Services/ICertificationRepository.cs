using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Services
{
    public interface ICertificationRepository
    {
        #region Owner Methods

        Task<List<OwnerModel>> FindAllOwners();

        Task<OwnerModel> FindOwnerById(Guid ownerId);

        Task<OwnerModel> FindOwnerByUsername(string username);

        Task<bool> DoesOwnerExist(Guid ownerId);

        Task<Guid> CreateOwner(CreateOwnerModel entity);

        Task<Guid> UpdateOwner(UpdateOwnerModel entity);

        Task<bool> DeleteOwner(Guid entity);

        #endregion

        #region Boat Methods

        Task<List<BoatModel>> FindAllBoats();

        Task<List<BoatModel>> FindAllBoatsByOwnerId(Guid ownerId);

        Task<BoatModel> FindBoatById(Guid boatId);

        Task<bool> DoesBoatExist(Guid boatId);

        Task<Guid> CreateBoat(CreateBoatModel entity);

        Task<Guid> UpdateBoat(UpdateBoatModel entity);

        Task<bool> DeleteBoat(Guid entity);

        #endregion

        #region Item Methods

        Task<List<ItemModel>> FindAllItems();

        Task<List<ItemModel>> FindItemsByBoatId(Guid boatId);

        Task<ItemModel> FindItemById(Guid id);

        Task<bool> DoesItemExist(Guid id);

        Task<Guid> CreateItem(CreateItemModel entity);

        Task<Guid> UpdateItem(UpdateItemModel entity);

        Task<bool> DeleteItem(Guid entity);

        #endregion
    }
}
