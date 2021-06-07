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
        public static DocumentMobileEntity ToImageDataEntity(DocumentMobileModel model)
        {
            var image = new DocumentMobileEntity()
            {
                FilePath = model.FilePath,
                Id = model.Id != null && model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                FileName = model.FileName,
                FileType = model.FileType,
                SystemId = model.SystemId,
                UniqueImageName = model.UniqueImageName
            };

            return image;
        }

        public static DocumentMobileModel ToImageModel(DocumentMobileEntity model)
        {
            var image = new DocumentMobileModel()
            {
                FilePath = model.FilePath,
                Id = model.Id != null && model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                FileName = model.FileName,
                FileType = model.FileType,
                SystemId = model.SystemId,
                UniqueImageName = model.UniqueImageName,
                FileContent = File.ReadAllBytes(model.FilePath)
            };

            return image;
        }
    }
}
