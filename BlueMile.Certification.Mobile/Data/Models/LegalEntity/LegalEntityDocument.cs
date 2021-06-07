using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
    public class LegalEntityDocument : IBaseDbEntity
    {
        #region Instance Properties
        
        /// <summary>
        /// Gets or sets the unique identifier of the document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LegalEntity"/> for the 
        /// current <see cref="LegalEntityDocument"/>.
        /// </summary>
        public LegalEntity LegalEntity { get; set; }

        /// <summary>
        /// Gets or sets the mime type identifying the type of data contained in the file.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique file name for the current <see cref="LegalEntityDocument"/> 
        /// used to uniquely identify the document in the back end document storage.
        /// </summary>
        public string UniqueFileName { get; set; }

        /// <summary>
        /// Gets or set the unique identifier for the <see cref="DocumentType"/> of 
        /// the current <see cref="LegalEntityDocument"/>.
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the legal entity associated with this document.
        /// </summary>
        public Guid LegalEntityId { get; set; }

        /// <summary>
        /// Gets or set the type of the current <see cref="LegalEntityDocument"/>.
        /// </summary>
        public DocumentType DocumentType { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="LegalEntityDocument"/>.
        /// </summary>
        public LegalEntityDocument()
        {

        }

        #endregion

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
