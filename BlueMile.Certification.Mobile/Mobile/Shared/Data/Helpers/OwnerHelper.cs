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
                SystemId = owner.SystemId,
                Town = owner.Town,
                IsSynced = owner.IsSynced,

                IcasaPopImageId = owner.IcasaPopPhoto?.Id,
                IdentificationDocumentId = owner.IdentificationDocument?.Id,
                SkippersLicenseImageId = owner.SkippersLicenseImage?.Id
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
                SystemId = owner.SystemId,
                Town = owner.Town,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                Id = owner.Id,
                IsSynced = owner.IsSynced,
            };
            return ownerModel;
        }
    }
}
