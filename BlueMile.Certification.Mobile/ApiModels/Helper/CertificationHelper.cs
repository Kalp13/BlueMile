using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Helper
{
    public static class CertificationHelper
    {
        public static CreateCertificationRequestModel ToCreateCertificationRequestModel(CertificationRequestModel model)
        {
            var request = new CreateCertificationRequestModel()
            {
                BoatId = model.BoatId,
                Id = model.Id,
                RequestStateId = model.RequestStateId
            };
            return request;
        }
    }
}
