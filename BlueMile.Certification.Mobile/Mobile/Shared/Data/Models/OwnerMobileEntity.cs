using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Models
{
    public class OwnerMobileEntity
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the owner.
        /// </summary>
        public string Surname { get; set; }

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
        /// Gets or sets the first line of the owner's address.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the owner's address.
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the third line of the owner's address.
        /// </summary>
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth line of the owner's address.
        /// </summary>
        public string AddressLine4 { get; set; }

        /// <summary>
        /// Gets or sets the suburb of the owner.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the town of the owner.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the province of the owner.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the country of the owner.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the owner.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the Identity Document <see cref="ImageMobileEntity"/>.
        /// </summary>
        public Guid? IdentificationDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the Skippers License <see cref="ImageMobileEntity"/>.
        /// </summary>
        public Guid? SkippersLicenseImageId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the Icasa POP <see cref="ImageMobileEntity"/>.
        /// </summary>
        public Guid? IcasaPopImageId { get; set; }

        /// <summary>
        /// Gets or sets the boolean flad indicating if this <see cref="OwnerMobileEntity"/>
        /// has been synchronized to the server.
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

        #endregion
    }
}
