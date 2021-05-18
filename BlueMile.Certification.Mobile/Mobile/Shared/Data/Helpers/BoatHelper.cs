using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Helpers
{
    public static class BoatHelper
    {
        public static BoatMobileEntity ToBoatEntity(BoatMobileModel boat)
        {
            var boatEntity = new BoatMobileEntity()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                Id = boat.Id != null && boat.Id != Guid.Empty ? boat.Id : Guid.NewGuid(),
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                SystemId = boat.SystemId,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                BoyancyCertificateImageId = boat.BoyancyCertificateImage?.Id,
                TubbiesCertificateImageId = boat.TubbiesCertificateImage?.Id
            };

            return boatEntity;
        }

        public static BoatMobileModel ToBoatModel(BoatMobileEntity boat)
        {
            var boatModel = new BoatMobileModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                Id = boat.Id,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                SystemId = boat.SystemId,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
            };

            return boatModel;
        }
    }
}
