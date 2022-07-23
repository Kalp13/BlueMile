using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    public class LegalEntityAddress : IBaseDbEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of this address.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique <see cref="LegalEntity"/> identifier for the 
        /// current <see cref="LegalEntityAddress"/>.
        /// </summary>
        public Guid LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LegalEntity"/> for the 
        /// current <see cref="LegalEntityAddress"/>.
        /// </summary>
        public LegalEntity LegalEntity { get; set; }

        /// <summary>
        /// Gets or sets the unit number of the complex or estate.
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the complex or estate.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Gets or sets the street number of the address.
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the street.
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the address.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the address.
        /// </summary>
        public decimal? Longitude { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="LegalEntityAddress"/>.
        /// </summary>
        public LegalEntityAddress()
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
