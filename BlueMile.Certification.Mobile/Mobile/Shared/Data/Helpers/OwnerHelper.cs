using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Helpers
{
    public static class OwnerHelper
    {
        public static OwnerMobileEntity ToOwnerDataEntity(OwnerMobileModel owner)
        {
            var OwnerMobileEntity = new OwnerMobileEntity()
            {
                Id = owner.Id != null && owner.Id != Guid.Empty ? owner.Id : Guid.NewGuid(),
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
                Name = owner.FirstName,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                Suburb = owner.Suburb,
                Surname = owner.LastName,
                Town = owner.Town,
                IsSynced = owner.IsSynced,
            };
            return OwnerMobileEntity;
        }

        public static OwnerMobileModel ToOwnerModel(OwnerMobileEntity owner)
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
                FirstName = owner.Name,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                Suburb = owner.Suburb,
                LastName = owner.Surname,
                Town = owner.Town,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                Id = owner.Id,
                IsSynced = owner.IsSynced,
            };
            return ownerModel;
        }

        public static OwnerDocumentMobileEntity ToOwnerDocumentEntity(OwnerDocumentMobileModel ownerDoc)
        {
            var doc = new OwnerDocumentMobileEntity()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                FileName = ownerDoc.FileName,
                Id = ownerDoc.Id,
                MimeType = ownerDoc.MimeType,
                OwnerId = ownerDoc.OwnerId,
                UniqueFileName = ownerDoc.UniqueFileName,
                FilePath = ownerDoc.FilePath
            };
            return doc;
        }

        public static OwnerDocumentMobileModel ToOwnerDocumentModel(OwnerDocumentMobileEntity ownerDoc)
        {
            var doc = new OwnerDocumentMobileModel()
            {
                DocumentTypeId = ownerDoc.DocumentTypeId,
                Id = ownerDoc.Id,
                FileName = ownerDoc.FileName,
                MimeType = ownerDoc.MimeType,
                OwnerId = ownerDoc.OwnerId,
                UniqueFileName = ownerDoc.UniqueFileName,
                FilePath = ownerDoc.FilePath,
            };
            return doc;
        }
    }
}
