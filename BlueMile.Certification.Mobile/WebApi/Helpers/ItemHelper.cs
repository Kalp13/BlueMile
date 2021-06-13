using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class ItemHelper
    {
        public static Item ToCreateItemModel(CreateItemModel createItem)
        {
            var item = new Item()
            {
                BoatId = createItem.BoatId,
                CapturedDate = createItem.CapturedDate,
                CreatedOn = DateTime.Now,
                CreatedBy = "test",
                Description = createItem.Description,
                ExpiryDate = createItem.ExpiryDate,
                Id = createItem.Id,
                IsActive = true,
                ItemTypeId = createItem.ItemTypeId,
                ModifiedOn = DateTime.Now,
                ModifiedBy = "test",
                SerialNumber = createItem.SerialNumber,
            };
            return item;
        }

        public static Item ToUpdateItemModel(Item existingItem, UpdateItemModel updateItem)
        {
            existingItem = new Item()
            {
                BoatId = updateItem.BoatId,
                CapturedDate = updateItem.CapturedDate,
                Description = updateItem.Description,
                ExpiryDate = updateItem.ExpiryDate,
                Id = updateItem.Id,
                IsActive = true,
                ItemTypeId = updateItem.ItemTypeId,
                ModifiedOn = DateTime.Now,
                ModifiedBy = "test",
                SerialNumber = updateItem.SerialNumber
            };
            return existingItem;
        }

        public static Item ToItemDataModel(ItemModel itemModel)
        {
            var item = new Item()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                Id = itemModel.Id,
                ItemTypeId = itemModel.ItemTypeId,
                SerialNumber = itemModel.SerialNumber
            };
            return item;
        }

        public static ItemModel ToItemApiModel(Item itemModel)
        {
            var item = new Web.ApiModels.ItemModel()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                Id = itemModel.Id,
                ItemTypeId = itemModel.ItemTypeId,
                SerialNumber = itemModel.SerialNumber
            };
            return item;
        }

        public static ItemDocument ToCreateDocumentModel(ItemDocumentModel model)
        {
            var doc = new ItemDocument()
            {
                CreatedBy = "test",
                CreatedOn = DateTime.Now,
                FileName = model.FileName,
                UniqueFileName = model.UniqueFileName,
                DocumentTypeId = model.DocumentTypeId,
                Id = model.Id,
                ItemId = model.ItemId,
                IsActive = true,
                MimeType = model.MimeType,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return doc;
        }

        public static ItemDocument ToUpdateDocumentModel(ItemDocumentModel model)
        {
            var doc = new ItemDocument()
            {
                FileName = model.FileName,
                UniqueFileName = model.UniqueFileName,
                DocumentTypeId = model.DocumentTypeId,
                Id = model.Id,
                ItemId = model.ItemId,
                MimeType = model.MimeType,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return doc;
        }
    }
}
