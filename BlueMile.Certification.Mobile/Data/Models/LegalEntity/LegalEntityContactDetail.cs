using System;

namespace BlueMile.Certification.Data.Models
{
    public class LegalEntityContactDetail : IBaseDbEntity
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the current unique <see cref="LegalEntityContactDetail"/> identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique <see cref="LegalEntity"/> identifier for the 
        /// current <see cref="LegalEntityContactDetail"/>.
        /// </summary>
        public Guid LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LegalEntity"/> for the 
        /// current <see cref="LegalEntityContactDetail"/>.
        /// </summary>
        public LegalEntity LegalEntity { get; set; }

		/// <summary>
		/// Gets or set the contact details value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="ContactDetailType"/> identifier for the 
		/// current <see cref="LegalEntityContactDetail"/>.
		/// </summary>
		public int ContactDetailTypeId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ContactDetailType"/> for the 
		/// current <see cref="LegalEntityContactDetail"/>.
		/// </summary>
		public ContactDetailType ContactDetailType { get; set; }

		/// <summary>
		/// Gets or sets if the current <see cref="LegalEntityContactDetail"/> should 
		/// be used primarily for the current associated <see cref="LegalEntity"/>.
		/// </summary>
		public bool IsPrimary { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new default <see cref="LegalEntityContactDetail"/>.
		/// </summary>
		public LegalEntityContactDetail()
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
