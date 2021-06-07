using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    /// <summary>
    /// <see cref="OwnerMobileModel"/> contains of all of the properties required of an owner for certifying his boats.
    /// </summary>
    public class OwnerMobileModel
    {
        #region Instance Fields

        /// <summary>
        /// Gets or sets the local unique identifier of the owner.
        /// </summary>
        [PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of this owner on the server.
        /// </summary>
        public Guid SystemId { get; set; }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the surname of the owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email of the owner.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the owner.
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the identification number of the owner.
        /// </summary>
        public string Identification { get; set; }

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
        /// Gets or sets the flag indicating if this owner has been synchronized to the server.
        /// </summary>
        public bool IsSynced { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        public DocumentMobileModel IcasaPopPhoto { get; set; }

        public DocumentMobileModel IdentificationDocument { get; set; }

        public DocumentMobileModel SkippersLicenseImage { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="OwnerMobileModel"/>.
        /// </summary>
        public OwnerMobileModel()
        {

        }

        #endregion

        #region Class Methods

        public override string ToString()
        {
            var message = $"{this.FirstName} {this.LastName}\n" +
                          $"{this.Identification}\n" +
                          $"{this.Email}\n" +
                          $"{this.ContactNumber}\n" +
                          $"{this.SkippersLicenseNumber}\n" +
                          $"{this.VhfOperatorsLicense}";
            return message;
        }

        #endregion
    }
}