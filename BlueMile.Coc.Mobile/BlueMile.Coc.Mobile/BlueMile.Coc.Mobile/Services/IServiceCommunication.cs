using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Coc.Mobile.Services
{
    public interface IServiceCommunication
    {
        #region Security Methods

        Task<bool> RegisterUser(UserEntity user);

        Task<UserEntity> LogUserIn(string username, string password);

        #endregion

        #region Owner Methods

        Task<OwnerEntity> GetOwnerByDetails(string username, string password);

        Task<OwnerEntity> CreateOwner(OwnerModel owner);

        Task<OwnerEntity> UpdateOwner(OwnerModel owner);

        #endregion

        #region Boat Methods

        Task<List<BoatEntity>> GetBoatsByOwnerId(Guid ownerId);

        Task<BoatEntity> GetBoatById(Guid boatId);

        Task<BoatEntity> CreateBoat(BoatModel boat);

        Task<BoatEntity> UpdateBoat(BoatModel boat);

        #endregion

        #region Item Methods

        Task<List<RequiredItemEntity>> GetBoatRequiredItems(Guid boatId);

        Task<RequiredItemEntity> CreateItem(RequiredItemModel item);

        Task<RequiredItemEntity> UpdateItem(RequiredItemModel item);

        #endregion

        #region Image Methods

        Task<ImageEntity> GetImageById(Guid imageId);

        Task<bool> CreateImage(ImageModel image);

        Task<bool> UpdateImage(ImageModel image);

        #endregion
    }
}
