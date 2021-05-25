using System;
using System.Collections.Generic;

namespace BlueMile.Certification.Data.Models
{
    public class OwnerModel : IBaseDbEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string Identification { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string Suburb { get; set; }
        
        public string Town { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="BoatModel"/>s
        /// associated to the current <see cref="OwnerModel"/>.
        /// </summary>		
        public ICollection<BoatModel> Boats { get; set; }

        #region Constructor

        public OwnerModel()
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
