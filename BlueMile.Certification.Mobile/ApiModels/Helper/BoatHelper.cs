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
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber
            };

            return createModel;
        }

        public static UpdateBoatModel ToUpdateBoatModel(BoatModel boat)
        {
            var updateModel = new UpdateBoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                Id = boat.Id,
                RegisteredNumber = boat.RegisteredNumber,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber
            };

            return updateModel;
        }

        public static CreateBoatDocumentModel ToCreateDocumentModel(BoatDocumentModel boatDoc)
        {
            var doc = new CreateBoatDocumentModel()
            {
                Id = boatDoc.Id,
                DocumentTypeId = boatDoc.DocumentTypeId,
                FileContent = boatDoc.FileContent,
                FileName = boatDoc.FileName,
                BoatId = boatDoc.BoatId,
                MimeType = boatDoc.MimeType,
                UniqueFileName = boatDoc.UniqueFileName
            };
            return doc;
        }

        public static UpdateBoatDocumentModel ToUpdateDocumentModel(BoatDocumentModel boatDoc)
        {
            var doc = new UpdateBoatDocumentModel()
            {
                Id = boatDoc.Id,
                DocumentTypeId = boatDoc.DocumentTypeId,
                FileContent = boatDoc.FileContent,
                FileName = boatDoc.FileName,
                BoatId = boatDoc.BoatId,
                MimeType = boatDoc.MimeType,
                UniqueFileName = boatDoc.UniqueFileName
            };
            return doc;
        }
    }
}
