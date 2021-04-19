using BlueMile.Certification.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WASM.Server.Models.Helpers
{
    public static class BoatHelper
    {
        public static BoatModel ToCreateBoatData(Web.ApiModels.BoatModel boatModel)
        {
            var boat = new BoatModel()
            {
                CategoryId = boatModel.BoatCategoryId,
                BoyancyCertificateNumber = boatModel.BoyancyCertificateNumber,
                IsJetski = boatModel.IsJetski,
                Name = boatModel.Name,
                OwnerId = boatModel.OwnerId,
                RegisteredNumber = boatModel.RegisteredNumber,
                Id = Guid.NewGuid(),
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber
            };

            return boat;
        }
        public static BoatModel ToUpdateBoatData(Web.ApiModels.BoatModel boatModel)
        {
            var boat = new BoatModel()
            {
                CategoryId = boatModel.BoatCategoryId,
                BoyancyCertificateNumber = boatModel.BoyancyCertificateNumber,
                IsJetski = boatModel.IsJetski,
                Name = boatModel.Name,
                OwnerId = boatModel.OwnerId,
                RegisteredNumber = boatModel.RegisteredNumber,
                Id = boatModel.SystemId,
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber
            };

            return boat;
        }

        public static BoatModel ToBoatDataModel(Web.ApiModels.BoatModel boatModel)
        {
            var boat = new BoatModel()
            {
                CategoryId = boatModel.BoatCategoryId,
                BoyancyCertificateNumber = boatModel.BoyancyCertificateNumber,
                IsJetski = boatModel.IsJetski,
                Name = boatModel.Name,
                OwnerId = boatModel.OwnerId,
                RegisteredNumber = boatModel.RegisteredNumber,
                Id = boatModel.SystemId,
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber
            };

            return boat;
        }

        public static Web.ApiModels.BoatModel ToApiBoatModel(BoatModel boatEntity)
        {
            var boat = new Web.ApiModels.BoatModel()
            {
                BoatCategoryId = boatEntity.CategoryId,
                BoyancyCertificateNumber = boatEntity.BoyancyCertificateNumber,
                IsJetski = boatEntity.IsJetski,
                Name = boatEntity.Name,
                OwnerId = boatEntity.OwnerId,
                RegisteredNumber = boatEntity.RegisteredNumber,
                SystemId = boatEntity.Id,
                TubbiesCertificateNumber = boatEntity.TubbiesCertificateNumber
            };
            return boat;
        }
    }
}
