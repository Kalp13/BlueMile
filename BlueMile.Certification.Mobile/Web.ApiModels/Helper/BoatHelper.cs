using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Helper
{
    public static class BoatHelper
    {
        public static CreateBoatModel ToCreateBoatModel(BoatModel boat)
        {
            var createModel = new CreateBoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = boat.BoyancyCertificateImage,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                TubbiesCertificateImage = boat.TubbiesCertificateImage,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber
            };

            return createModel;
        }

        public static UpdateBoatModel ToUpdateBoatModel(BoatModel boat)
        {
            var updateModel = new UpdateBoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = boat.BoyancyCertificateImage,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                TubbiesCertificateImage = boat.TubbiesCertificateImage,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber
            };

            return updateModel;
        }
    }
}
