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
                IsSynced = boat.IsSynced,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
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
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                IsSynced = boat.IsSynced
            };

            return boatModel;
        }

        public static BoatDocumentMobileEntity ToBoatDocumentEntity(BoatDocumentMobileModel boatDoc)
        {
            var doc = new BoatDocumentMobileEntity()
            {
                DocumentTypeId = boatDoc.DocumentTypeId,
                FileName = boatDoc.FileName,
                Id = boatDoc.Id,
                MimeType = boatDoc.MimeType,
                BoatId = boatDoc.BoatId,
                UniqueFileName = boatDoc.UniqueFileName,
            };
            return doc;
        }

        public static BoatDocumentMobileModel ToBoatDocumentModel(BoatDocumentMobileEntity boatDoc)
        {
            var doc = new BoatDocumentMobileModel()
            {
                DocumentTypeId = boatDoc.DocumentTypeId,
                Id = boatDoc.Id,
                FileName = boatDoc.FileName,
                MimeType = boatDoc.MimeType,
                BoatId = boatDoc.BoatId,
                UniqueFileName = boatDoc.UniqueFileName,
            };
            return doc;
        }
    }
}
