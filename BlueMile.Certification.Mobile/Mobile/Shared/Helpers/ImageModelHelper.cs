using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;

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
                Id = image.SystemId,
                UniqueImageName = image.UniqueImageName,
                FileContent = image.FileContent
            };

            return imageModel;
        }

        public static ImageModel ToImageModel(ImageMobileModel image)
        {
            var imageModel = new ImageModel()
            {
                FilePath = image.FilePath,
                ImageName = image.ImageName,
                SystemId = image.Id,
                UniqueImageName = image.UniqueImageName,
                FileContent = image.FileContent
            };

            return imageModel;
        }
    }
}
