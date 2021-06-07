using BlueMile.Certification.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class ItemHelper
    {
        public static Item ToCreateItemModel(Web.ApiModels.CreateItemModel createItem)
        {
            var item = new Item()
            {
                BoatId = createItem.BoatId,
                CapturedDate = createItem.CapturedDate,
                CreatedOn = DateTime.Now,
                Description = createItem.Description,
                ExpiryDate = createItem.ExpiryDate,
                Id = Guid.NewGuid(),
                IsActive = true,
                ItemTypeId = createItem.ItemTypeId,
                ModifiedOn = DateTime.Now,
                SerialNumber = createItem.SerialNumber,
            };
            return item;
        }

        public static Item ToUpdateItemModel(Web.ApiModels.UpdateItemModel updateItem)
        {
            var item = new Item()
            {
                BoatId = updateItem.BoatId,
                CapturedDate = updateItem.CapturedDate,
                Description = updateItem.Description,
                ExpiryDate = updateItem.ExpiryDate,
                Id = updateItem.SystemId,
                IsActive = true,
                ItemTypeId = updateItem.ItemTypeId,
                ModifiedOn = DateTime.Now,
                SerialNumber = updateItem.SerialNumber
            };
            return item;
        }

        public static Item ToItemDataModel(Web.ApiModels.ItemModel itemModel)
        {
            var item = new Item()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                Id = itemModel.SystemId,
                ItemTypeId = itemModel.ItemTypeId,
                ModifiedOn = DateTime.Now,
                SerialNumber = itemModel.SerialNumber
            };
            return item;
        }

        public static Web.ApiModels.ItemModel ToItemApiModel(Item itemModel)
        {
            var item = new Web.ApiModels.ItemModel()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                SystemId = itemModel.Id,
                ItemTypeId = itemModel.ItemTypeId,
                SerialNumber = itemModel.SerialNumber
            };
            return item;
        }
    }
}
