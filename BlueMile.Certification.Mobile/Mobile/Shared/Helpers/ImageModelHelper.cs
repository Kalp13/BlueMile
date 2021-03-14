using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Helpers
{
    public class ImageModelHelper
    {
        public static ImageMobileModel ToImageMobileModel(ImageModel image)
        {
            var imageModel = new ImageMobileModel()
            {
                FilePath = image.FilePath,
                ImageName = image.ImageName,
                SystemId = image.SystemId,
                UniqueImageName = image.UniqueImageName
            };

            return imageModel;
        }

        public static ImageModel ToImageModel(ImageMobileModel image)
        {
            var imageModel = new ImageModel()
            {
                FilePath = image.FilePath,
                ImageName = image.ImageName,
                SystemId = image.SystemId,
                UniqueImageName = image.UniqueImageName
            };

            return imageModel;
        }
    }
}
