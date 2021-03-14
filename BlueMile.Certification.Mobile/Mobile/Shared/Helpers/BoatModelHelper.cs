using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Helpers
{
    public static class BoatModelHelper
    {
        public static BoatMobileModel ToMobileModel(BoatModel boat)
        {
            var boatModel = new BoatMobileModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = ImageModelHelper.ToImageMobileModel(boat.BoyancyCertificateImage),
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                SystemId = boat.SystemId,
                TubbiesCertificateImage = ImageModelHelper.ToImageMobileModel(boat.TubbiesCertificateImage),
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber
            };

            return boatModel;
        }

        public static BoatModel ToModel(BoatMobileModel boat)
        {
            var boatModel = new BoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = ImageModelHelper.ToImageModel(boat.BoyancyCertificateImage),
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                SystemId = boat.SystemId,
                TubbiesCertificateImage = ImageModelHelper.ToImageModel(boat.TubbiesCertificateImage),
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber
            };
            return boatModel;
        }
    }
}
