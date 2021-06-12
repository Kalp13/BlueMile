using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class BoatHelper
    {
        public static Boat ToCreateBoatData(CreateBoatModel boatModel)
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
                Id = boatModel.Id,
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber,
                CreatedBy = "test",
                ModifiedBy = "test",
            };

            return boat;
        }

        public static Boat ToUpdateBoatData(Boat existingBoat, UpdateBoatModel boatModel)
        {
            if (existingBoat is null)
            {
                throw new ArgumentNullException(nameof(existingBoat));
            }

            existingBoat = new Boat()
            {
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now,
                CategoryId = boatModel.BoatCategoryId,
                BoyancyCertificateNumber = boatModel.BoyancyCertificateNumber,
                IsJetski = boatModel.IsJetski,
                Name = boatModel.Name,
                OwnerId = boatModel.OwnerId,
                RegisteredNumber = boatModel.RegisteredNumber,
                Id = boatModel.Id,
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber
            };

            return existingBoat;
        }

        public static Boat ToBoatDataModel(BoatModel boatModel)
        {
            var boat = new Boat()
            {
                CategoryId = boatModel.BoatCategoryId,
                BoyancyCertificateNumber = boatModel.BoyancyCertificateNumber,
                IsJetski = boatModel.IsJetski,
                Name = boatModel.Name,
                OwnerId = boatModel.OwnerId,
                RegisteredNumber = boatModel.RegisteredNumber,
                Id = boatModel.Id,
                TubbiesCertificateNumber = boatModel.TubbiesCertificateNumber
            };

            return boat;
        }

        public static BoatModel ToApiBoatModel(Boat boatEntity)
        {
            var boat = new Web.ApiModels.BoatModel()
            {
                BoatCategoryId = boatEntity.CategoryId,
                BoyancyCertificateNumber = boatEntity.BoyancyCertificateNumber,
                IsJetski = boatEntity.IsJetski,
                Name = boatEntity.Name,
                OwnerId = boatEntity.OwnerId,
                RegisteredNumber = boatEntity.RegisteredNumber,
                Id = boatEntity.Id,
                TubbiesCertificateNumber = boatEntity.TubbiesCertificateNumber
            };
            return boat;
        }

        public static BoatDocument ToCreateDocumentModel(BoatDocumentModel model)
        {
            var doc = new BoatDocument()
            {
                CreatedBy = "test",
                CreatedOn = DateTime.Now,
                FileName = model.FileName,
                UniqueFileName = model.UniqueFileName,
                DocumentTypeId = model.DocumentTypeId,
                Id = model.Id,
                BoatId = model.BoatId,
                IsActive = true,
                MimeType = model.MimeType,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return doc;
        }

        public static BoatDocument ToUpdateDocumentModel(BoatDocumentModel model)
        {
            var doc = new BoatDocument()
            {
                FileName = model.FileName,
                UniqueFileName = model.UniqueFileName,
                DocumentTypeId = model.DocumentTypeId,
                Id = model.Id,
                BoatId = model.BoatId,
                MimeType = model.MimeType,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return doc;
        }
    }
}
