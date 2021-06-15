using BlueMile.Certification.Data.Models.StaticData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
    public class CertificationRequest : IBaseDbEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="CertificationRequest"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the boat associated with this image.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Boat"/> for the current <see cref="CertificationRequest"/>.
        /// </summary>
        public Boat Boat { get; set; }

        public int RequestStateId { get; set; }

        public RequestState RequestState { get; set; }

        public DateTime? RequestedOn { get; set; }

        public DateTime? RejectedOn { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public DateTime? CompletedOn { get; set; }

        #region IBaseDbEntity Implementation

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="CertificationRequest"/>.
        /// </summary>
        public CertificationRequest()
        {

        }

        #endregion
    }
}
