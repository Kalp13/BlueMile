using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Helpers
{
    public static class ItemModelHelper
    {
        public static ItemMobileModel ToItemMobileModel(ItemModel item)
        {
            var itemModel = new ItemMobileModel()
            {
                BoatId = item.BoatId,
                CapturedDate = item.CapturedDate,
                Description = item.Description,
                ExpiryDate = item.ExpiryDate,
                ItemImage = item.ItemImage != null ? ToOwnerDocumentMobileModel(item.ItemImage) : null,
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber,
                Id = item.Id,
                IsSynced = item.IsSynced
            };

            return itemModel;
        }

        public static ItemModel ToItemModel(ItemMobileModel item)
        {
            var itemModel = new ItemModel()
            {
                BoatId = item.BoatId,
                CapturedDate = item.CapturedDate,
                Description = item.Description,
                ExpiryDate = item.ExpiryDate,
                ItemImage = item.ItemImage != null ? ToOwnerDocumentModel(item.ItemImage) : null,
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber,
                Id = item.Id,
                IsSynced = item.IsSynced
            };

            return itemModel;
        }

        public static ItemDocumentMobileModel ToOwnerDocumentMobileModel(ItemDocumentModel itemDoc)
        {
            var doc = new ItemDocumentMobileModel()
            {
                DocumentTypeId = itemDoc.DocumentTypeId,
                FileName = itemDoc.FileName,
                Id = itemDoc.Id,
                ItemId = itemDoc.ItemId,
                MimeType = itemDoc.MimeType,
                UniqueFileName = itemDoc.UniqueFileName,
                FilePath = itemDoc.FilePath
            };
            return doc;
        }

        public static ItemDocumentModel ToOwnerDocumentModel(ItemDocumentMobileModel itemDoc)
        {
            var doc = new ItemDocumentModel()
            {
                DocumentTypeId = itemDoc.DocumentTypeId,
                FileName = itemDoc.FileName,
                Id = itemDoc.Id,
                ItemId = itemDoc.ItemId,
                MimeType = itemDoc.MimeType,
                UniqueFileName = itemDoc.UniqueFileName,
                FilePath = itemDoc.FilePath
            };
            return doc;
        }
    }
}
