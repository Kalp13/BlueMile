using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Helper
{
    public static class ItemHelper
    {
        public static CreateItemModel ToCreateItemModel(ItemModel item)
        {
            var itemModel = new CreateItemModel()
            {
                BoatId = item.BoatId,
                CapturedDate = item.CapturedDate,
                Description = item.Description,
                ExpiryDate = item.ExpiryDate,
                ItemImage = item.ItemImage,
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber
            };

            return itemModel;
        }

        public static UpdateItemModel ToUpdateItemModel(ItemModel item)
        {
            var itemModel = new UpdateItemModel()
            {
                BoatId = item.BoatId,
                CapturedDate = item.CapturedDate,
                Description = item.Description,
                ExpiryDate = item.ExpiryDate,
                Id = item.Id,
                ItemImage = item.ItemImage,
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber
            };

            return itemModel;
        }

        public static CreateItemDocumentModel ToCreateDocumentModel(ItemDocumentModel itemDoc)
        {
            var doc = new CreateItemDocumentModel()
            {
                Id = itemDoc.Id,
                DocumentTypeId = itemDoc.DocumentTypeId,
                FileContent = itemDoc.FileContent,
                FileName = itemDoc.FileName,
                ItemId = itemDoc.ItemId,
                MimeType = itemDoc.MimeType,
                UniqueFileName = itemDoc.UniqueFileName
            };
            return doc;
        }

        public static UpdateItemDocumentModel ToUpdateDocumentModel(ItemDocumentModel itemDoc)
        {
            var doc = new UpdateItemDocumentModel()
            {
                Id = itemDoc.Id,
                DocumentTypeId = itemDoc.DocumentTypeId,
                FileContent = itemDoc.FileContent,
                FileName = itemDoc.FileName,
                ItemId = itemDoc.ItemId,
                MimeType = itemDoc.MimeType,
                UniqueFileName = itemDoc.UniqueFileName
            };
            return doc;
        }
    }
}
