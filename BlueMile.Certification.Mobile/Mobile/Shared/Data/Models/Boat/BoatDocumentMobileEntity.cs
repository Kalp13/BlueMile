using SQLite;
using System;

namespace BlueMile.Certification.Mobile.Data.Models
{
    public class BoatDocumentMobileEntity
    {
        [PrimaryKey]
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the boat associated with this document.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the mime type identifying the type of data contained in the file.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique file name for the current <see cref="BoatDocumentMobileEntity"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="BoatDocumentMobileEntity"/>.
        /// </summary>
        public int DocumentTypeId { get; set; }
    }
}
