using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Helper
{
    public static class ImageHelper
    {
        public static CreateImageModel ToCreateImageModel(ImageModel image)
        {
            var imageModel = new CreateImageModel()
            {
                FilePath = image.FilePath,
                ImageName = image.ImageName,
                UniqueImageName = image.UniqueImageName
            };

            return imageModel; 
        }

        public static UpdateImageModel ToUpdateImageModel(ImageModel image)
        {
            var imageModel = new UpdateImageModel()
            {
                FilePath = image.FilePath,
                ImageName = image.ImageName,
                UniqueImageName = image.UniqueImageName
            };

            return imageModel;
        }
    }
}
