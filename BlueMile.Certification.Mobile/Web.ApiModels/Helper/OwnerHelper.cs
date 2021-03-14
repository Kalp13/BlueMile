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
                AddressLine1 = owner.AddressLine1,
                AddressLine2 = owner.AddressLine2,
                AddressLine3 = owner.AddressLine3,
                AddressLine4 = owner.AddressLine4,
                ContactNumber = owner.ContactNumber,
                Country = owner.Country,
                Email = owner.Email,
                IcasaPopPhoto = owner.IcasaPopPhoto,
                Identification = owner.Identification,
                IdentificationDocument = owner.IdentificationDocument,
                Name = owner.Name,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                SkippersLicenseImage = owner.SkippersLicenseImage,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                Suburb = owner.Suburb,
                Surname = owner.Surname,
                Town = owner.Town,
                VhfOperatorsLicense = owner.VhfOperatorsLicense
            };

            return ownerModel;
        }

        public static UpdateOwnerModel ToUpdateOwnerModel(OwnerModel owner)
        {
            var ownerModel = new UpdateOwnerModel()
            {
                AddressLine1 = owner.AddressLine1,
                AddressLine2 = owner.AddressLine2,
                AddressLine3 = owner.AddressLine3,
                AddressLine4 = owner.AddressLine4,
                ContactNumber = owner.ContactNumber,
                Country = owner.Country,
                Email = owner.Email,
                IcasaPopPhoto = owner.IcasaPopPhoto,
                Identification = owner.Identification,
                IdentificationDocument = owner.IdentificationDocument,
                Name = owner.Name,
                PostalCode = owner.PostalCode,
                Province = owner.Province,
                SkippersLicenseImage = owner.SkippersLicenseImage,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                Suburb = owner.Suburb,
                Surname = owner.Surname,
                Town = owner.Town,
                VhfOperatorsLicense = owner.VhfOperatorsLicense
            };

            return ownerModel;
        }
    }
}
