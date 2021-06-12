using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class OwnerHelper
    {
        public static IndividualOwner ToCreateOwnerModel(CreateOwnerModel model)
        {
            var owner = new IndividualOwner()
            {
                Identification = model.Identification,
                LastName = model.LastName,
                FirstName = model.FirstName,
                SkippersLicenseNumber = model.SkippersLicenseNumber,
                VhfOperatorsLicense = model.VhfOperatorsLicense,
                Id = model.Id,
                CreatedBy = "test",
                CreatedOn = DateTime.Now,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now,
                IsActive = true,
            };

            return owner;
        }

        public static IndividualOwner ToUpdateOwnerModel(UpdateOwnerModel model)
        {
            var owner = new IndividualOwner()
            {
                Identification = model.Identification,
                LastName = model.LastName,
                FirstName = model.FirstName,
                SkippersLicenseNumber = model.SkippersLicenseNumber,
                VhfOperatorsLicense = model.VhfOperatorsLicense,
                Id = model.Id,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return owner;
        }

        public static LegalEntityAddress ToCreateAddressModel(CreateOwnerModel model)
        {
            var address = new LegalEntityAddress()
            {
                UnitNumber = model.UnitNumber,
                ComplexName = model.ComplexName,
                StreetNumber = model.StreetNumber,
                StreetName = model.StreetName,
                Suburb = model.Suburb,
                Town = model.Town,
                Province = model.Province,
                Country = model.Country,
                PostalCode = model.PostalCode,
                ModifiedBy = model.Email,
                ModifiedOn = DateTime.Now,
                LegalEntityId = model.Id,
                Id = Guid.NewGuid(),
                CreatedBy = model.Email,
                CreatedOn = DateTime.Now,
                IsActive = true,
            };
            return address;
        }

        public static LegalEntityAddress ToUpdateAddressModel(UpdateOwnerModel model)
        {
            var address = new LegalEntityAddress()
            {
                UnitNumber = model.UnitNumber,
                ComplexName = model.ComplexName,
                StreetNumber = model.StreetNumber,
                StreetName = model.StreetName,
                Suburb = model.Suburb,
                Town = model.Town,
                Province = model.Province,
                Country = model.Country,
                PostalCode = model.PostalCode,

                Id = model.Id,
                LegalEntityId = model.Id,
                ModifiedOn = DateTime.Now,
                ModifiedBy = model.Email,
            };
            return address;
        }

        public static OwnerModel ToApiOwnerModel(IndividualOwner ownerEntity)
        {
            var owner = new Web.ApiModels.OwnerModel()
            {
                Identification = ownerEntity.Identification,
                FirstName = ownerEntity.FirstName,
                LastName = ownerEntity.LastName,
                SkippersLicenseNumber = ownerEntity.SkippersLicenseNumber,
                VhfOperatorsLicense = ownerEntity.VhfOperatorsLicense,
                
                Id = ownerEntity.Id,
            };

            return owner;
        }

        public static LegalEntityDocument ToCreateDocumentModel(OwnerDocumentModel model)
        {
            var doc = new LegalEntityDocument()
            {
                CreatedBy = "test",
                CreatedOn = DateTime.Now,
                FileName = model.FileName,
                UniqueFileName = model.UniqueFileName,
                DocumentTypeId = model.DocumentTypeId,
                Id = model.Id,
                LegalEntityId = model.LegalEntityId,
                IsActive = true,
                MimeType = model.MimeType,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return doc;
        }

        public static LegalEntityDocument ToUpdateDocumentModel(OwnerDocumentModel model)
        {
            var doc = new LegalEntityDocument()
            {
                FileName = model.FileName,
                UniqueFileName = model.UniqueFileName,
                DocumentTypeId = model.DocumentTypeId,
                Id = model.Id,
                LegalEntityId = model.LegalEntityId,
                MimeType = model.MimeType,
                ModifiedBy = "test",
                ModifiedOn = DateTime.Now
            };

            return doc;
        }
    }
}
