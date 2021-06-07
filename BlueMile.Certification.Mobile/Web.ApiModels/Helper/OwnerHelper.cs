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
                IcasaPopPhoto = owner.IcasaPopPhoto,
                Identification = owner.Identification,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                IdentificationDocument = owner.IdentificationDocument,
                SkippersLicenseImage = owner.SkippersLicenseImage,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                VhfOperatorsLicense = owner.VhfOperatorsLicense
            };

            return ownerModel;
        }

        public static UpdateOwnerModel ToUpdateOwnerModel(OwnerModel owner)
        {
            var ownerModel = new UpdateOwnerModel()
            {
                IcasaPopPhoto = owner.IcasaPopPhoto,
                Identification = owner.Identification,
                IdentificationDocument = owner.IdentificationDocument,
                FirstName = owner.FirstName,
                SkippersLicenseImage = owner.SkippersLicenseImage,
                SkippersLicenseNumber = owner.SkippersLicenseNumber,
                LastName = owner.LastName,
                SystemId = owner.SystemId,
                VhfOperatorsLicense = owner.VhfOperatorsLicense
            };

            return ownerModel;
        }
    }
}
