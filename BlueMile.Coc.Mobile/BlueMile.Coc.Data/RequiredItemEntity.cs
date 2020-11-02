using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Coc.Data
{
    public class RequiredItemEntity
    {
        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="RequiredItemEntity"/>.
        /// </summary>
        [PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="BoatEntity"/> for this <see cref="RequiredItemEntity"/>.
        /// </summary>
        [Indexed]
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the type of item for the current boat.
        /// </summary>
        [Indexed]
        public ItemTypeStaticEntity ItemTypeId { get; set; }

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
    }
}
