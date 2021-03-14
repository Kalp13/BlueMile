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
                ItemImage = ImageModelHelper.ToImageMobileModel(item.ItemImage),
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber,
                SystemId = item.SystemId
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
                ItemImage = ImageModelHelper.ToImageModel(item.ItemImage),
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber,
                SystemId = item.SystemId
            };

            return itemModel;
        }
    }
}
