using System;
using System.Collections.Generic;
using System.Text;
using BlueMile.Certification.Data.Static;

namespace BlueMile.Certification.Data.Models
{
    public class ItemModel : IBaseDbEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="ItemModel"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="BoatModel"/> for this <see cref="ItemModel"/>.
        /// </summary>
        public Guid BoatId { get; set; }

        public BoatModel Boat { get; set; }

        /// <summary>
        /// Gets or sets the type of item for the current boat.
        /// </summary>
        public int ItemType { get; set; }

        /// <summary>
        /// Gets or sets the serial number of the item.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date and time that this item was created.
        /// </summary>
        public DateTime CapturedDate { get; set; }

        /// <summary>
        /// Gets or sets the date that this item expires.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the path to the image for this item.
        /// </summary>
        public Guid ItemImageId { get; set; }

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
