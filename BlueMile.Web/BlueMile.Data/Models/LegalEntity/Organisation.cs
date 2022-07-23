namespace BlueMile.Data.Models
{
    public class Organisation : LegalEntity, IBaseDbEntity
	{
		#region Instance Properties

		/// <summary>
		/// Gets or set the name of the current <see cref="Organisation"/>.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or set the legal trading as name of the current <see cref="Organisation"/>.
		/// </summary>
		public string TradingAs { get; set; }

		/// <summary>
		/// Gets or set the registration number of the current <see cref="Organisation"/>.
		/// </summary>
		public string RegistrationNumber { get; set; }

		/// <summary>
		/// Gets or sets of the current <see cref="Organisation"/> is VAT registered.
		/// </summary>
		public bool IsVATResigtered { get; set; }

		/// <summary>
		/// Gets or set the VAT number of the current <see cref="Organisation"/>.
		/// </summary>
		public string VatNumber { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="OrganisationType"/> identifier for the 
		/// current <see cref="Organisation"/>.
		/// </summary>
		public int OrganisationTypeId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="OrganisationType"/> for the current <see cref="Organisation"/>.
		/// </summary>
		public OrganisationType OrganisationType { get; set; }

		/// <summary>
		/// Gets or sets the collection of <see cref="OrganisationUnit"/>s
		/// associated with the current <see cref="Organisation"/>.
		/// </summary>
		public ICollection<OrganisationUnit> OrganisationUnits { get; set; }

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
