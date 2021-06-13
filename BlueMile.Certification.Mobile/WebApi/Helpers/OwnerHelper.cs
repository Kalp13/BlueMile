using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.IO;
using System.Linq;

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

        public static LegalEntityAddress ToUpdateAddressModel(UpdateOwnerModel model, Guid ownerId)
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

                Id = ownerId,
                LegalEntityId = model.Id,
                ModifiedOn = DateTime.Now,
                ModifiedBy = model.Email,
            };
            return address;
        }

        public static OwnerModel ToApiOwnerModel(IndividualOwner ownerEntity, LegalEntityAddress address, LegalEntityContactDetail[] contactDetails, LegalEntityDocument[] documents)
        {
            var phone = contactDetails?.FirstOrDefault(x => x.ContactDetailTypeId == (int)ContactDetailTypeEnum.Phone);
            var mobile = contactDetails?.FirstOrDefault(x => x.ContactDetailTypeId == (int)ContactDetailTypeEnum.MobileNumber);
            var email = contactDetails?.FirstOrDefault(x => x.ContactDetailTypeId == (int)ContactDetailTypeEnum.EmailAddress);

            var icasaDoc = documents?.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.IcasaProofOfPayment);
            var idDoc = documents?.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.IdentificationDocument);
            var skippersDoc = documents?.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.SkippersLicense);

            var owner = new OwnerModel()
            {
                Identification = ownerEntity.Identification,
                FirstName = ownerEntity.FirstName,
                LastName = ownerEntity.LastName,
                SkippersLicenseNumber = ownerEntity.SkippersLicenseNumber,
                VhfOperatorsLicense = ownerEntity.VhfOperatorsLicense,
                Id = ownerEntity.Id,

                UnitNumber = address?.UnitNumber,
                ComplexName = address?.ComplexName,
                StreetNumber = address?.StreetNumber,
                StreetName = address?.StreetName,
                Suburb = address?.Suburb,
                Town = address?.Town,
                Province = address?.Province,
                Country = address?.Country,
                PostalCode = address?.PostalCode,

                ContactNumber = mobile?.Value,
                Email = email?.Value,

                IcasaPopPhoto = icasaDoc != null ? OwnerHelper.ToApiOwnerDocumentModel(icasaDoc) : null,
                IdentificationDocument = idDoc != null ? OwnerHelper.ToApiOwnerDocumentModel(idDoc) : null,
                SkippersLicenseImage = skippersDoc != null ? OwnerHelper.ToApiOwnerDocumentModel(skippersDoc) : null
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
                ModifiedOn = DateTime.Now,
                FilePath = model.FilePath,
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
                ModifiedOn = DateTime.Now,
                FilePath = model.FilePath,
            };

            return doc;
        }

        public static OwnerDocumentModel ToApiOwnerDocumentModel(LegalEntityDocument model)
        {
            var doc = new OwnerDocumentModel()
            {
                DocumentTypeId = model.DocumentTypeId,
                FileContent = File.ReadAllBytes(model.FilePath),
                FileName = model.FileName,
                FilePath = model.FilePath,
                Id = model.Id,
                LegalEntityId = model.LegalEntityId,
                MimeType = model.MimeType,
                UniqueFileName = model.UniqueFileName,
            };
            return doc;
        }
    }
}
