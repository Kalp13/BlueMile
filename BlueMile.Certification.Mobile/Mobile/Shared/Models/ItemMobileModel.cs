using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class ItemMobileModel
    {
        [AutoIncrement, PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="RequiredItemEntity"/>.
        /// </summary>
        public Guid SystemId { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="BoatEntity"/> for this <see cref="RequiredItemEntity"/>.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the type of item for the current boat.
        /// </summary>
        public int ItemTypeId { get; set; }

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
        public ImageMobileModel ItemImage { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating if this item has been synchronized to the server.
        /// </summary>
        public bool IsSynced { get; set; }
    }
}
