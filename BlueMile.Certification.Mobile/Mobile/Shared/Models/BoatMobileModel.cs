using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class BoatMobileModel
    {
        /// <summary>
        /// Gets or sets the auto incremented primary key for this boat.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public int BoatCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public BoatDocumentMobileModel BoyancyCertificateImage { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public BoatDocumentMobileModel TubbiesCertificateImage { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating if this boat has been synchronized to the server.
        /// </summary>
        public bool IsSynced { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="BoatMobileModel"/>.
        /// </summary>
        public BoatMobileModel()
        {

        }

        #endregion

        #region Class Methods

        public override string ToString()
        {
            return $"{this.Name}\n{this.RegisteredNumber}\n{this.BoyancyCertificateNumber}\n" + (this.IsJetski ? this.TubbiesCertificateNumber : "");
        }

        #endregion
    }

    public class BoatDocumentMobileModel
    {
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
        /// Gets or sets the unique file name for the current <see cref="BoatDocumentMobileModel"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="BoatDocumentMobileModel"/>.
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
