using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Helpers
{
    public static class ItemHelper
    {
        public static ItemMobileEntity ToItemEntity(ItemMobileModel item)
        {
            var itemEntity = new ItemMobileEntity()
            {
                BoatId = item.BoatId,
                CapturedDate = item.CapturedDate,
                Description = item.Description,
                ExpiryDate = item.ExpiryDate,
                Id = item.Id != null && item.Id != Guid.Empty ? item.Id : Guid.NewGuid(),
                ItemImageId = item.ItemImage?.Id,
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber,
                SystemId = item.SystemId,
                IsSynced = item.IsSynced,
            };

            return itemEntity;
        }

        public static ItemMobileModel ToItemModel(ItemMobileEntity item)
        {
            var itemModel = new ItemMobileModel()
            {
                BoatId = item.BoatId,
                CapturedDate = item.CapturedDate,
                Description = item.Description,
                ExpiryDate = item.ExpiryDate,
                Id = item.Id,
                ItemTypeId = item.ItemTypeId,
                SerialNumber = item.SerialNumber,
                SystemId = item.SystemId,
                IsSynced = item.IsSynced
            };

            return itemModel;
        }
    }
}
