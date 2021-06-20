using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class CertificationRequestModel
    {
        public Guid Id { get; set; }

        public Guid BoatId { get; set; }

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
