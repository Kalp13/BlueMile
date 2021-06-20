using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class CertificationRequestModel
    {
        public Guid Id { get; set; }

        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the name of the boat linked to this request.
        /// </summary>
        public string BoatName { get; set; }

        public int RequestStateId { get; set; }

        public DateTime? RequestedOn { get; set; }

        public DateTime? RejectedOn { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public DateTime? CompletedOn { get; set; }
    }

    public class CreateCertificationRequestModel
    {
        public Guid Id { get; set; }

        public Guid BoatId { get; set; }

        public int RequestStateId { get; set; }
    }
}
