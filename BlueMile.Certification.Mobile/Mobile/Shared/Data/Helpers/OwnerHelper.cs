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
                Id = owner.Id,
                AddressLine1 = owner.AddressLine1,
                AddressLine2 = owner.AddressLine2,
                AddressLine3 = owner.AddressLine3,
                AddressLine4 = owner.AddressLine4,
                ContactNumber = owner.ContactNumber,
                Country = owner.Country,
                Email = owner.Email,
                Identification = owner.Identification,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                Name = owner.Name,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                Suburb = owner.Suburb,
                Surname = owner.Surname,
                SystemId = owner.SystemId,
                Town = owner.Town,

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
                AddressLine1 = owner.AddressLine1,
                AddressLine2 = owner.AddressLine2,
                AddressLine3 = owner.AddressLine3,
                AddressLine4 = owner.AddressLine4,
                ContactNumber = owner.ContactNumber,
                Country = owner.Country,
                Email = owner.Email,
                Identification = owner.Identification,
                Name = owner.Name,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                Suburb = owner.Suburb,
                Surname = owner.Surname,
                SystemId = owner.SystemId,
                Town = owner.Town,
                VhfOperatorsLicense = owner.VhfOperatorsLicense,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                Id = owner.Id,
            };
            return ownerModel;
        }
    }
}
