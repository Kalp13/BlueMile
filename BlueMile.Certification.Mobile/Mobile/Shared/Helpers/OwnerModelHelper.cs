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
                IcasaPopPhoto = ImageModelHelper.ToImageMobileModel(owner.IcasaPopPhoto),
                IdentificationDocument = ImageModelHelper.ToImageMobileModel(owner.IdentificationDocument),
                IsSynced = owner.IsSynced,
                SkippersLicenseImage = ImageModelHelper.ToImageMobileModel(owner.SkippersLicenseImage)
            };

            return ownerModel;
        }

        public static OwnerModel ToOwnerModel(OwnerMobileModel owner)
        {
            var ownerModel = new OwnerModel()
            {
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
                //IcasaPopPhoto = ImageModelHelper.ToImageModel(owner.IcasaPopPhoto),
                //IdentificationDocument = ImageModelHelper.ToImageModel(owner.IdentificationDocument),
                IsSynced = owner.IsSynced,
                //SkippersLicenseImage = ImageModelHelper.ToImageModel(owner.SkippersLicenseImage)
            };

            return ownerModel;
        }
    }
}
