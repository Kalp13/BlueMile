using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.IO;
using System.Linq;

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

        public static Item ToUpdateItemModel(UpdateItemModel updateItem)
        {
            var item = new Item()
            {
                BoatId = updateItem.BoatId,
                CapturedDate = updateItem.CapturedDate,
                Description = updateItem.Description,
                ExpiryDate = updateItem.ExpiryDate,
                IsActive = true,
                ItemTypeId = updateItem.ItemTypeId,
                SerialNumber = updateItem.SerialNumber
            };
            return item;
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

        public static ItemModel ToItemApiModel(Item itemModel, ItemDocument[]? documents)
        {
            var itemDoc = documents.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.Photo);
            var item = new ItemModel()
            {
                BoatId = itemModel.BoatId,
                CapturedDate = itemModel.CapturedDate,
                Description = itemModel.Description,
                ExpiryDate = itemModel.ExpiryDate,
                Id = itemModel.Id,
                ItemTypeId = itemModel.ItemTypeId,
                SerialNumber = itemModel.SerialNumber,

                ItemImage = itemDoc != null ? ToApiItemDocumentModel(itemDoc) : null
            };
            return item;
        }

        public static ItemDocument ToItemDocument(ItemDocumentModel model)
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

        public static ItemDocumentModel ToApiItemDocumentModel(ItemDocument model)
        {
            var document = new ItemDocumentModel()
            {
                DocumentTypeId = model.DocumentTypeId,
                FileContent = File.ReadAllBytes(model.FilePath),
                FileName = model.FileName,
                Id = model.Id,
                ItemId = model.ItemId,
                MimeType = model.MimeType,
                UniqueFileName = model.UniqueFileName
            };
            return document;
        }
    }
}
