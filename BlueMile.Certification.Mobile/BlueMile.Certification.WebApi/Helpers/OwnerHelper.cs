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
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
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
                VhfOperatorsLicense = model.VhfOperatorsLicense
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

                Id = Guid.NewGuid(),
                CreatedBy = model.Email,
                CreatedOn = DateTime.Now,
                IsActive = true,
            };
            return address;
        }

        public static IndividualOwner ToOwnerDataModel(OwnerModel model)
        {
            var owner = new IndividualOwner()
            {
                Identification = model.Identification,
                LastName = model.LastName,
                FirstName = model.FirstName,
                SkippersLicenseNumber = model.SkippersLicenseNumber,
                VhfOperatorsLicense = model.VhfOperatorsLicense,
                Id = model.SystemId,
            };

            return owner;
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
                SystemId = ownerEntity.Id,
            };

            return owner;
        }
    }
}
