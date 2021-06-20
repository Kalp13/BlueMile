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
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                BoyancyCertificateImage = boat.BoyancyCertificateImage != null ? ToBoatDocumentMobileModel(boat.BoyancyCertificateImage) : null,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                Id = boat.Id,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                TubbiesCertificateImage = boat.TubbiesCertificateImage != null ? ToBoatDocumentMobileModel(boat.TubbiesCertificateImage) : null,
                IsSynced = boat.IsSynced
            };

            return boatModel;
        }

        public static BoatModel ToModel(BoatMobileModel boat)
        {
            var boatModel = new BoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                BoyancyCertificateImage = boat.BoyancyCertificateImage != null ? ToBoatDocumentModel(boat.BoyancyCertificateImage) : null,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                Id = boat.Id,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                TubbiesCertificateImage = boat.TubbiesCertificateImage != null ? ToBoatDocumentModel(boat.TubbiesCertificateImage) : null,
                IsSynced = boat.IsSynced
            };
            return boatModel;
        }

        public static BoatDocumentMobileModel ToBoatDocumentMobileModel(BoatDocumentModel ownerDoc)
        {
            var doc = new BoatDocumentMobileModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                BoatId = ownerDoc.BoatId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName,
                FilePath = ownerDoc.FilePath
            };
            return doc;
        }

        public static BoatDocumentModel ToBoatDocumentModel(BoatDocumentMobileModel ownerDoc)
        {
            var doc = new BoatDocumentModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                BoatId = ownerDoc.BoatId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName,
                FilePath = ownerDoc.FilePath
            };
            return doc;
        }

        public static CertificationRequestMobileModel ToCertificationRequestMobileModel(CertificationRequestModel model)
        {
            var request = new CertificationRequestMobileModel()
            {
                ApprovedOn = model.ApprovedOn,
                BoatId = model.BoatId,
                CompletedOn = model.CompletedOn,
                Id = model.Id,
                RejectedOn = model.RejectedOn,
                RequestedOn = model.RequestedOn,
                RequestStateId = model.RequestStateId
            };
            return request;
        }

        public static CertificationRequestModel ToCertificationRequestModel(CertificationRequestMobileModel model)
        {
            var request = new CertificationRequestModel()
            {
                ApprovedOn = model.ApprovedOn,
                BoatId = model.BoatId,
                CompletedOn = model.CompletedOn,
                Id = model.Id,
                RejectedOn = model.RejectedOn,
                RequestedOn = model.RequestedOn,
                RequestStateId = model.RequestStateId
            };
            return request;
        }
    }
}
