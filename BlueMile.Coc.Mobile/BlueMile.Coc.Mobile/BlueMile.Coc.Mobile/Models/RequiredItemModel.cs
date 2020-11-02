using BlueMile.Coc.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Coc.Mobile.Models
{
    public class RequiredItemModel
    {
        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="RequiredItemModel"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="BoatModel"/> for this <see cref="RequiredItemModel"/>.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the type of item for the current boat.
        /// </summary>
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
        public ImageModel ItemImage { get; set; }

        public RequiredItemModel()
        {
            this.ItemImage = new ImageModel();
        }
    }
}
