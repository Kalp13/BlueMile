using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;

namespace BlueMile.Certification.Mobile.Helpers
{
    public static class BoatModelHelper
    {
        public static BoatMobileModel ToMobileModel(BoatModel boat)
        {
            var boatModel = new BoatMobileModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = boat.BoyancyCertificateImage != null ? ToOwnerDocumentMobileModel(boat.BoyancyCertificateImage) : null,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                Id = boat.Id,
                TubbiesCertificateImage = boat.TubbiesCertificateImage != null ? ToOwnerDocumentMobileModel(boat.TubbiesCertificateImage) : null,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                IsSynced = boat.IsSynced
            };

            return boatModel;
        }

        public static BoatModel ToModel(BoatMobileModel boat)
        {
            var boatModel = new BoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = boat.BoyancyCertificateImage != null ? ToOwnerDocumentModel(boat.BoyancyCertificateImage) : null,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                Id = boat.Id,
                TubbiesCertificateImage = boat.TubbiesCertificateImage != null ? ToOwnerDocumentModel(boat.TubbiesCertificateImage) : null,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                IsSynced = boat.IsSynced
            };
            return boatModel;
        }

        public static BoatDocumentMobileModel ToOwnerDocumentMobileModel(BoatDocumentModel ownerDoc)
        {
            var doc = new BoatDocumentMobileModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                BoatId = ownerDoc.BoatId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName
            };
            return doc;
        }

        public static BoatDocumentModel ToOwnerDocumentModel(BoatDocumentMobileModel ownerDoc)
        {
            var doc = new BoatDocumentModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                BoatId = ownerDoc.BoatId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName
            };
            return doc;
        }
    }
}
