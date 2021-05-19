using BlueMile.Certification.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class ItemHelper
    {
        public static ItemModel ToCreateItemModel(Web.ApiModels.CreateItemModel createItem)
        {
            var item = new ItemModel()
            {
                BoatId = createItem.BoatId,
                CapturedDate = createItem.CapturedDate,
                CreatedOn = DateTime.Now,
                Description = createItem.Description,
                ExpiryDate = createItem.ExpiryDate,
                Id = Guid.NewGuid(),
                IsActive = true,
                ItemType = createItem.ItemTypeId,
                ModifiedOn = DateTime.Now,
                SerialNumber = createItem.SerialNumber,
            };
            return item;
        }

        public static ItemModel ToUpdateItemModel(Web.ApiModels.UpdateItemModel updateItem)
        {
            var item = new ItemModel()
            {
                BoatId = updateItem.BoatId,
                CapturedDate = updateItem.CapturedDate,
                Description = updateItem.Description,
                ExpiryDate = updateItem.ExpiryDate,
                Id = updateItem.SystemId,
                IsActive = true,
                ItemType = updateItem.ItemTypeId,
                ModifiedOn = DateTime.Now,
                SerialNumber = updateItem.SerialNumber
            };
            return item;
        }

        public static ItemModel ToItemDataModel(Web.ApiModels.ItemModel itemModel)
        {
            var item = new ItemModel()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                Id = itemModel.SystemId,
                ItemImageId = itemModel.ItemImage.SystemId,
                ItemType = itemModel.ItemTypeId,
                ModifiedOn = DateTime.Now,
                SerialNumber = itemModel.SerialNumber
            };
            return item;
        }

        public static Web.ApiModels.ItemModel ToItemApiModel(ItemModel itemModel)
        {
            var item = new Web.ApiModels.ItemModel()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                SystemId = itemModel.Id,
                ItemTypeId = itemModel.ItemType,
                SerialNumber = itemModel.SerialNumber
            };
            return item;
        }
    }
}
