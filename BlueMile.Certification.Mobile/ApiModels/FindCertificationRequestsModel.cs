using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class FindCertificationRequestsModel
    {
        public string SearchTerm { get; set; }

        public Guid? BoatId { get; set; }

        public Guid? CertificationRequestId { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="FindCertificationRequestsModel"/>.
        /// </summary>
        public FindCertificationRequestsModel()
        {

        }

        #endregion
    }
}
