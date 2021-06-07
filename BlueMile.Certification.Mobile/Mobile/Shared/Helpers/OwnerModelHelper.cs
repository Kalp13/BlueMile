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
                SystemId = owner.SystemId,
                Town = owner.Town,
                IcasaPopPhoto = owner.IcasaPopPhoto != null ? ImageModelHelper.ToImageMobileModel(owner.IcasaPopPhoto) : null,
                IdentificationDocument = owner.IdentificationDocument != null ? ImageModelHelper.ToImageMobileModel(owner.IdentificationDocument) : null,
                IsSynced = owner.IsSynced,
                SkippersLicenseImage = owner.SkippersLicenseImage != null ? ImageModelHelper.ToImageMobileModel(owner.SkippersLicenseImage) : null 
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
                SystemId = owner.SystemId,
                Town = owner.Town,
                IcasaPopPhoto = owner.IcasaPopPhoto != null ? ImageModelHelper.ToImageModel(owner.IcasaPopPhoto) : null,
                IdentificationDocument = owner.IdentificationDocument != null ? ImageModelHelper.ToImageModel(owner.IdentificationDocument) : null,
                IsSynced = owner.IsSynced,
                SkippersLicenseImage = owner.SkippersLicenseImage != null ? ImageModelHelper.ToImageModel(owner.SkippersLicenseImage) : null
            };

            return ownerModel;
        }
    }
}
