using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;

namespace BlueMile.Certification.Mobile.Helpers
{
    public static class BoatModelHelper
    {
        public static BoatMobileModel ToMobileModel(BoatModel boat)
        {
            var boatModel = new BoatMobileModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = boat.BoyancyCertificateImage != null ? ImageModelHelper.ToImageMobileModel(boat.BoyancyCertificateImage) : null,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                SystemId = boat.SystemId,
                TubbiesCertificateImage = boat.TubbiesCertificateImage != null ? ImageModelHelper.ToImageMobileModel(boat.TubbiesCertificateImage) : null,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                IsSynced = boat.IsSynced
            };

            return boatModel;
        }

        public static BoatModel ToModel(BoatMobileModel boat)
        {
            var boatModel = new BoatModel()
            {
                BoatCategoryId = boat.BoatCategoryId,
                BoyancyCertificateImage = boat.BoyancyCertificateImage != null ? ImageModelHelper.ToImageModel(boat.BoyancyCertificateImage) : null,
                BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                IsJetski = boat.IsJetski,
                Name = boat.Name,
                OwnerId = boat.OwnerId,
                RegisteredNumber = boat.RegisteredNumber,
                SystemId = boat.SystemId,
                TubbiesCertificateImage = boat.TubbiesCertificateImage != null ? ImageModelHelper.ToImageModel(boat.TubbiesCertificateImage) : null,
                TubbiesCertificateNumber = boat.TubbiesCertificateNumber,
                IsSynced = boat.IsSynced
            };
            return boatModel;
        }
    }
}
