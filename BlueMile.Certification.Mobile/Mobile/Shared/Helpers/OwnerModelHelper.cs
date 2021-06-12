using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Helpers
{
    public static class OwnerModelHelper
    {
        public static OwnerMobileModel ToOwnerMobileModel(OwnerModel owner)
        {
            var ownerModel = new OwnerMobileModel()
            {
                UnitNumber = owner.UnitNumber,
                ComplexName = owner.ComplexName,
                StreetNumber = owner.StreetNumber,
                StreetName = owner.StreetName,
                ContactNumber = owner.ContactNumber,
                Country = owner.Country,
                Email = owner.Email,
                Identification = owner.Identification,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                FirstName = owner.FirstName,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                Suburb = owner.Suburb,
                LastName = owner.LastName,
                Id = owner.Id,
                Town = owner.Town,
                IcasaPopPhoto = owner.IcasaPopPhoto != null ? ToOwnerDocumentMobileModel(owner.IcasaPopPhoto) : null,
                IdentificationDocument = owner.IdentificationDocument != null ? ToOwnerDocumentMobileModel(owner.IdentificationDocument) : null,
                IsSynced = owner.IsSynced,
                SkippersLicenseImage = owner.SkippersLicenseImage != null ? ToOwnerDocumentMobileModel(owner.SkippersLicenseImage) : null 
            };

            return ownerModel;
        }

        public static OwnerModel ToOwnerModel(OwnerMobileModel owner)
        {
            var ownerModel = new OwnerModel()
            {
                UnitNumber = owner.UnitNumber,
                ComplexName = owner.ComplexName,
                StreetNumber = owner.StreetNumber,
                StreetName = owner.StreetName,
                ContactNumber = owner.ContactNumber,
                Country = owner.Country,
                Email = owner.Email,
                Identification = owner.Identification,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                FirstName = owner.FirstName,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                Suburb = owner.Suburb,
                LastName = owner.LastName,
                Id = owner.Id,
                Town = owner.Town,
                IcasaPopPhoto = owner.IcasaPopPhoto != null ? ToOwnerDocumentModel(owner.IcasaPopPhoto) : null,
                IdentificationDocument = owner.IdentificationDocument != null ? ToOwnerDocumentModel(owner.IdentificationDocument) : null,
                IsSynced = owner.IsSynced,
                SkippersLicenseImage = owner.SkippersLicenseImage != null ? ToOwnerDocumentModel(owner.SkippersLicenseImage) : null
            };

            return ownerModel;
        }

        public static OwnerDocumentMobileModel ToOwnerDocumentMobileModel(OwnerDocumentModel ownerDoc)
        {
            var doc = new OwnerDocumentMobileModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                OwnerId = ownerDoc.LegalEntityId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName
            };
            return doc;
        }

        public static OwnerDocumentModel ToOwnerDocumentModel(OwnerDocumentMobileModel ownerDoc)
        {
            var doc = new OwnerDocumentModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                LegalEntityId = ownerDoc.OwnerId,
                MimeType = ownerDoc.MimeType,
                UniqueFileName = ownerDoc.UniqueFileName
            };
            return doc;
        }
    }
}
