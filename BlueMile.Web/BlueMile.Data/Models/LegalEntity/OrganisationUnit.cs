namespace BlueMile.Data.Models
{
    public class OrganisationUnit : LegalEntity, IBaseDbEntity
	{
		#region Instance Properties

		/// <summary>
		/// Gets or set the name of the current <see cref="OrganisationUnit"/>.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or set the legal trading as name of the current <see cref="OrganisationUnit"/>.
		/// </summary>
		public string TradingAs { get; set; }

		/// <summary>
		/// Gets or set the registration number of the current <see cref="OrganisationUnit"/>.
		/// </summary>
		public string RegistrationNumber { get; set; }

		/// <summary>
		/// Gets or sets of the current <see cref="OrganisationUnit"/> is VAT registered.
		/// </summary>
		public bool IsVATResigtered { get; set; }

		/// <summary>
		/// Gets or set the VAT number of the current <see cref="OrganisationUnit"/>.
		/// </summary>
		public string VatNumber { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="OrganisationType"/> identifier for the 
		/// current <see cref="OrganisationUnit"/>.
		/// </summary>
		public int OrganisationTypeId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="OrganisationType"/> for 
		/// the current <see cref="OrganisationUnit"/>.
		/// </summary>
		public OrganisationType OrganisationType { get; set; }

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
