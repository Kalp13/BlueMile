using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlueMile.Coc.Data
{ 
    /// <summary>
    /// Represents the data structure for a boat.
    /// </summary>
    public class BoatEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this <see cref="BoatEntity"/>.
        /// </summary>
        [Required, PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatEntity"/>.
        /// </summary>
        [Indexed, Required]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        [Required]
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatEntity"/>.
        /// </summary>
        [Indexed, Required]
        public CategoryStaticEntity CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        [Required]
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        [Required]
        public Guid BoyancyCertificateImageId { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        [Required]
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public Guid TubbiesCertificateImageId { get; set; }
    }
}
