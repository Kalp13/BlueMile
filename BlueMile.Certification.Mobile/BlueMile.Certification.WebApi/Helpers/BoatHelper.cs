using BlueMile.Certification.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class BoatHelper
    {
        public static Boat ToCreateBoatData(Web.ApiModels.CreateBoatModel boatModel)
        {
            var boat = new Boat()
            {
                CategoryId = boatModel.BoatCategoryId,
                BoyancyCertificateNumber = boatModel.BoyancyCertificateNumber,
                IsJetski = boatModel.IsJetski,
                Name = boatModel.Name,
                CreatedOn = DateTime.Now,
                IsActive = true,
                ModifiedOn = DateTime.Now,
                OwnerId = boatModel.OwnerId,
                RegisteredNumber = boatModel.RegisteredNumber,
                Id = Guid.NewGuid(),
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber
            };

            return boat;
        }

        public static Boat ToUpdateBoatData(Web.ApiModels.UpdateBoatModel boatModel)
        {
            var boat = new Boat()
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

        public static Boat ToBoatDataModel(Web.ApiModels.BoatModel boatModel)
        {
            var boat = new Boat()
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

        public static Web.ApiModels.BoatModel ToApiBoatModel(Boat boatEntity)
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
