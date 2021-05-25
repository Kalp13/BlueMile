using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Helpers
{
    public static class ImageHelper
    {
        public static ImageMobileEntity ToImageDataEntity(ImageMobileModel model)
        {
            var image = new ImageMobileEntity()
            {
                FilePath = model.FilePath,
                Id = model.Id != null && model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                ImageName = model.ImageName,
                SystemId = model.SystemId,
                UniqueImageName = model.UniqueImageName
            };

            return image;
        }

        public static ImageMobileModel ToImageModel(ImageMobileEntity model)
        {
            var image = new ImageMobileModel()
            {
                FilePath = model.FilePath,
                Id = model.Id != null && model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                ImageName = model.ImageName,
                SystemId = model.SystemId,
                UniqueImageName = model.UniqueImageName,
                FileContent = File.ReadAllBytes(model.FilePath)
            };

            return image;
        }
    }
}
