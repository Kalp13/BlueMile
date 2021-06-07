using System;
using System.Collections.Generic;
using System.Text;
using BlueMile.Certification.Data.Static;

namespace BlueMile.Certification.Data.Models
{
    public class Item : IBaseDbEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="Item"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="Models.Boat"/> for this <see cref="Item"/>.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the boat associated with the item.
        /// </summary>
        public Boat Boat { get; set; }

        /// <summary>
        /// Gets or sets the type of item for the current boat.
        /// </summary>
        public int ItemTypeId { get; set; }

        public ItemType ItemType { get; set; }

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
        /// Gets or sets the collection of <see cref="ItemDocument"/>s
        /// associated with the current <see cref="Item"/>.
        /// </summary>
        public ICollection<ItemDocument> Documents { get; set; }

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
