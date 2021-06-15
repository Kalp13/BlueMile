using System;
using System.Collections.Generic;
using System.Text;
using BlueMile.Certification.Data.Static;

namespace BlueMile.Certification.Data.Models
{
    public class Boat : IBaseDbEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this <see cref="Boat"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="Boat"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        public IndividualOwner Owner { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="Boat"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="Boat"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="Boat"/>.
        /// </summary>
        public int CategoryId { get; set; }

        public BoatCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="Boat"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="Boat"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="BoatDocument"/>s
        /// associated with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<BoatDocument> Documents { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Item"/>s associated with this <see cref="Boat"/>.
        /// </summary>
        public ICollection<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="CertificationRequest"/>s associated with this <see cref="Boat"/>.
        /// </summary>
        public ICollection<CertificationRequest> CertificationRequests { get; set; }

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
    }
}
