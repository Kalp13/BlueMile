using System;
using System.Collections.Generic;

namespace BlueMile.Certification.Data.Models
{
    public class IndividualOwner : LegalEntity, IBaseDbEntity
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the first name of the owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Boat"/>s
        /// associated to the current <see cref="IndividualOwner"/>.
        /// </summary>		
        public ICollection<Boat> Boats { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityAddress"/>s
        /// associated with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<LegalEntityAddress> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityContactDetail"/>s
        /// associated with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<LegalEntityContactDetail> ContactDetails { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="IndividualOwner"/>.
        /// </summary>
        public IndividualOwner()
        {

        }

        #endregion

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
