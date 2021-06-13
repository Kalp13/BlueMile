using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Helper
{
    public static class OwnerHelper
    {
        public static CreateOwnerModel ToCreateOwnerModel(OwnerModel owner)
        {
            var ownerModel = new CreateOwnerModel()
            {
                IcasaPopPhoto = owner.IcasaPopPhoto,
                Identification = owner.Identification,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                IdentificationDocument = owner.IdentificationDocument,
                SkippersLicenseImage = owner.SkippersLicenseImage,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,

                UnitNumber = owner.UnitNumber,
                ComplexName = owner.ComplexName,
                StreetNumber = owner.StreetNumber,
                StreetName = owner.StreetName,
                Suburb = owner.Suburb,
                Town = owner.Town,
                Province = owner.Province,
                Country = owner.Country,
                PostalCode = owner.PostalCode,

                ContactNumber = owner.ContactNumber,
                Id = owner.Id,
                Email = owner.Email
            };

            return ownerModel;
        }

        public static UpdateOwnerModel ToUpdateOwnerModel(OwnerModel owner)
        {
            var ownerModel = new UpdateOwnerModel()
            {
                Identification = owner.Identification,
                FirstName = owner.FirstName,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                LastName = owner.LastName,
                Id = owner.Id,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                
                UnitNumber = owner.UnitNumber,
                ComplexName = owner.ComplexName,
                StreetNumber = owner.StreetNumber,
                StreetName = owner.StreetName,
                Suburb = owner.Suburb,
                Town = owner.Town,
                Province = owner.Province,
                Country = owner.Country,
                PostalCode = owner.PostalCode,
                
                ContactNumber = owner.ContactNumber,
                Email = owner.Email
            };

            return ownerModel;
        }

        public static CreateOwnerDocumentModel ToCreateDocumentModel(OwnerDocumentModel ownerDoc)
        {
            var doc = new CreateOwnerDocumentModel()
            {
                Id = ownerDoc.Id,
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileContent = ownerDoc.FileContent,
                FileName = ownerDoc.FileName,
                LegalEntityId = ownerDoc.LegalEntityId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName
            };
            return doc;
        }

        public static UpdateOwnerDocumentModel ToUpdateDocumentModel(OwnerDocumentModel ownerDoc)
        {
            var doc = new UpdateOwnerDocumentModel()
            {
                Id = ownerDoc.Id,
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileContent = ownerDoc.FileContent,
                FileName = ownerDoc.FileName,
                LegalEntityId = ownerDoc.LegalEntityId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName
            };
            return doc;
        }
    }
}
