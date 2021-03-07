using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Helpers
{
    public static class ModelHelper
    {
        public static OwnerMobileEntity ToOwnerEntity(OwnerMobileModel owner)
        {
            var convertedOwner = new OwnerMobileEntity()
            {

            };

            return convertedOwner;
        }

        public static OwnerMobileModel ToOwnerModel(OwnerMobileEntity owner)
        {
            var convertedOwner = new OwnerMobileModel()
            {

            };

            return convertedOwner;
        }

        public static BoatMobileEntity ToBoatEntity(BoatMobileModel boat)
        {
            var convertedBoat = new BoatMobileEntity()
            {

            };
            return convertedBoat;
        }

        public static BoatMobileModel ToBoatModel(BoatMobileEntity boat)
        {
            var convertedBoat = new BoatMobileModel()
            {

            };
            return convertedBoat;
        }

        public static ImageMobileEntity ToImageEntity(ImageMobileModel image)
        {
            var convertedImage = new ImageMobileEntity()
            {

            };
            return convertedImage;
        }

        public static ImageMobileModel ToImageModel(ImageMobileEntity image)
        {
            var convertedImage = new ImageMobileModel()
            {

            };
            return convertedImage;
        }
    }
}
