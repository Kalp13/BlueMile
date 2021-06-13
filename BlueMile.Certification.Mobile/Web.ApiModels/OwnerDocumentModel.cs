using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class OwnerDocumentModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the legal entity associated with this document.
        /// </summary>
        public Guid LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the mime type identifying the type of data contained in the file.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique file name for the current <see cref="OwnerDocumentModel"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="OwnerDocumentModel"/>.
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the byte array containing the content of the image.
        /// </summary>
        public byte[] FileContent { get; set; }

        /// <summary>
        /// Gets or sets the location string to the document.
        /// </summary>
        public string FilePath { get; set; }
    }

    public class CreateOwnerDocumentModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the legal entity associated with this document.
        /// </summary>
        public Guid LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the mime type identifying the type of data contained in the file.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique file name for the current <see cref="OwnerDocumentModel"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="OwnerDocumentModel"/>.
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the byte array containing the content of the image.
        /// </summary>
        public byte[] FileContent { get; set; }
    }

    public class UpdateOwnerDocumentModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the legal entity associated with this document.
        /// </summary>
        public Guid LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the mime type identifying the type of data contained in the file.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique file name for the current <see cref="OwnerDocumentModel"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="OwnerDocumentModel"/>.
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the byte array containing the content of the image.
        /// </summary>
        public byte[] FileContent { get; set; }
    }
}