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
        public ItemDocumentMobileModel ItemImage { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating if this item has been synchronized to the server.
        /// </summary>
        public bool IsSynced { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="ItemMobileModel"/>.
        /// </summary>
        public ItemMobileModel()
        {

        }

        #endregion

        #region Class Methods

        public override string ToString()
        {
            return $"{this.Description}\n{this.SerialNumber}\n{this.CapturedDate}\n{this.ExpiryDate}";
        }

        #endregion
    }

    public class ItemDocumentMobileModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the boat associated with this document.
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Gets or sets the mime type identifying the type of data contained in the file.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique file name for the current <see cref="OwnerDocumentMobileModel"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="OwnerDocumentMobileModel"/>.
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the location string to the document.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the byte array containing the content of the image.
        /// </summary>
        public byte[] FileContent { get; set; }
    }
}
