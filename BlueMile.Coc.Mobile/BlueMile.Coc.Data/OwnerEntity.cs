using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace BlueMile.Coc.Data
{
    /// <summary>
    /// Represents the data structure for an Owner.
    /// </summary>
    public class OwnerEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this owner.
        /// </summary>
        [Required, PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this Owner.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>Surname</c> of this Owner.
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the <c>CellNumber</c> of this Owner.
        /// </summary>
        [Required]
        public string CellNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>Email</c> of this Owner.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the <c>Address</c> of this Owner.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        [Required]
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        [Required]
        public string SkippersLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseImagePath</c> of this Owner.
        /// </summary>
        [Required]
        public Guid SkippersLicenseImageId { get; set; }

        /// <summary>
        /// Gets or sets the <c>IdentityNumber</c> of this Owner.
        /// </summary>
        [Required]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>IdentityDocumentPath</c> of this Owner.
        /// </summary>
        [Required]
        public Guid IdentificationDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the <c>IcasaPopPhotoPath</c> of this Owner.
        /// </summary>
        [Required]
        public Guid IcasaPopPhotoId { get; set; }

        [Required]
        public bool IsSynced { get; set; }

        public long UserId { get; set; }
    }
}
