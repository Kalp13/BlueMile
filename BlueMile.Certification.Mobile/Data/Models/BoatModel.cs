using System;
using System.Collections.Generic;
using System.Text;
using BlueMile.Certification.Data.Static;

namespace BlueMile.Certification.Data.Models
{
    public class BoatModel : IBaseDbEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this <see cref="BoatModel"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatModel"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        public OwnerModel Owner { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatModel"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatModel"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatModel"/>.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatModel"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatModel"/>.
        /// </summary>
        public Guid BoyancyCertificateImageId { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatModel"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatModel"/>.
        /// </summary>
        public Guid TubbiesCertificateImageId { get; set; }

        public ICollection<ItemModel> Items { get; set; }

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
