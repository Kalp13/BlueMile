using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class CertificationRequestMobileModel
    {
        /// <summary>
        /// Gets or sets the unique primary identifier of the <see cref="CertificationRequestMobileModel"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="BoatMobileModel"/>
        /// for this <see cref="CertificationRequestMobileModel"/>.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the name of the boat linked to this request.
        /// </summary>
        public string BoatName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the state that this 
        /// <see cref="CertificationRequestMobileModel"/> is currently in.
        /// </summary>
        public int RequestStateId { get; set; }

        /// <summary>
        /// Gets or sets the date that this <see cref="CertificationRequestMobileModel"/> was requested.
        /// </summary>
        public DateTime? RequestedOn { get; set; }

        /// <summary>
        /// Gets or sets the date that this <see cref="CertificationRequestMobileModel"/> was rejected.
        /// </summary>
        public DateTime? RejectedOn { get; set; }

        /// <summary>
        /// Gets or sets the date that this <see cref="CertificationRequestMobileModel"/> was approved.
        /// </summary>
        public DateTime? ApprovedOn { get; set; }

        /// <summary>
        /// Gets or sets the date that this <see cref="CertificationRequestMobileModel"/> was completed.
        /// </summary>
        public DateTime? CompletedOn { get; set; }

        /// <summary>
        /// Gets or sets the date that this <see cref="CertificationRequestMobileModel"/> was cancelled.
        /// </summary>
        public DateTime? CancelledOn { get; set; }
    }
}
