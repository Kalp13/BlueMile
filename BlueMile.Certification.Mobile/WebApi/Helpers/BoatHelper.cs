using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class BoatHelper
    {
        /// <summary>
        /// Converts all a <see cref="CreateBoatModel"/> into a <see cref="Boat"/> entity.
        /// </summary>
        /// <param name="boatModel">
        ///     The <see cref="CreateBoatModel"/> to convert.
        /// </param>
        /// <returns>
        ///     Returns a new <see cref="Boat"/> object converted from the given input.
        /// </returns>
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

        public static Boat ToUpdateBoatData(UpdateBoatModel boatModel)
        {
            var boat = new Boat()
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

            return boat;
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

        public static BoatModel ToApiBoatModel(Boat boatEntity, BoatDocument[] documents)
        {
            var boyancyDoc = documents.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.BoatBoyancyCertificate);
            var tubbiesDoc = documents.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.TubbiesBoyancyCertificate);
            var boat = new BoatModel()
            {
                BoatCategoryId = boatEntity.CategoryId,
                BoyancyCertificateNumber = boatEntity.BoyancyCertificateNumber,
                IsJetski = boatEntity.IsJetski,
                Name = boatEntity.Name,
                OwnerId = boatEntity.OwnerId,
                RegisteredNumber = boatEntity.RegisteredNumber,
                Id = boatEntity.Id,
                TubbiesCertificateNumber = boatEntity.TubbiesCertificateNumber,

                BoyancyCertificateImage = boyancyDoc != null ? ToApiBoatDocumentModel(boyancyDoc) : null,
                TubbiesCertificateImage = tubbiesDoc != null ? ToApiBoatDocumentModel(tubbiesDoc) : null
            };
            return boat;
        }

        public static BoatDocument ToBoatDocument(BoatDocumentModel model)
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

        public static BoatDocumentModel ToApiBoatDocumentModel(BoatDocument model)
        {
            var document = new BoatDocumentModel()
            {
                BoatId = model.BoatId,
                DocumentTypeId = model.DocumentTypeId,
                FileContent = File.ReadAllBytes(model.FilePath),
                Id = model.Id,
                FileName = model.FileName,
                MimeType = model.MimeType,
                UniqueFileName = model.UniqueFileName
            };
            return document;
        }

        public static CertificationRequest ToCertificationDataModel(CreateCertificationRequestModel model)
        {
            var request = new CertificationRequest()
            {
                Id = model.Id,
                BoatId = model.BoatId,
                RequestStateId = model.RequestStateId,
                RequestedOn = DateTime.Now,
            };
            return request;
        }
    }
}
