﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class ItemModel
    {
        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="ItemModel"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="ItemModel"/> for this <see cref="ItemModel"/>.
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
        public ItemDocumentModel ItemImage { get; set; }

        public bool IsSynced { get; set; }
    }

    public class CreateItemModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item to create.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="ItemModel"/> for this <see cref="CreateItemModel"/>.
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
        public ItemDocumentModel ItemImage { get; set; }
    }

    public class UpdateItemModel
    {
        /// <summary>
        /// Gets or sets the primary unique identifier for a <see cref="UpdateItemModel"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique indexed foreign key to the <see cref="ItemModel"/> for this <see cref="UpdateItemModel"/>.
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
        public ItemDocumentModel ItemImage { get; set; }
    }
}