using System;
using System.Collections.Generic;
using System.Text;
using BlueMile.Certification.Data.Static;

namespace BlueMile.Certification.Data.Models
{
    public class BoatModel
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this <see cref="BoatEntity"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatEntity"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatEntity"/>.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public Guid BoyancyCertificateImageId { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatEntity"/>.
        /// </summary>
        public Guid TubbiesCertificateImageId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
