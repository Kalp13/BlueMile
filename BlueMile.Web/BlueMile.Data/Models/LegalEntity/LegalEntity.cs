namespace BlueMile.Data.Models
{
    public class LegalEntity
    {
		#region Instance Properties

		/// <summary>
		/// Gets or sets the unique identifier for the current <see cref="LegalEntity"/>.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the collection of child <see cref="LegalEntityRelationship"/>s
		/// associated with the current <see cref="LegalEntity"/>.
		/// </summary>
		public ICollection<LegalEntityRelationship> Children { get; set; }

		/// <summary>
		/// Gets or sets the collection of parent <see cref="LegalEntityRelationship"/>s
		/// associated with the current <see cref="LegalEntity"/>.
		/// </summary>
		public ICollection<LegalEntityRelationship> Parents { get; set; }

		/// <summary>
		/// Gets or sets the collection of <see cref="LegalEntityAddress"/>s
		/// associated with the current <see cref="LegalEntity"/>.
		/// </summary>
		public ICollection<LegalEntityAddress> Addresses { get; set; }

		/// <summary>
		/// Gets or sets the collection of <see cref="LegalEntityContactDetails"/>s
		/// associated with the current <see cref="LegalEntity"/>.
		/// </summary>
		public ICollection<LegalEntityContactDetail> ContactDetails { get; set; }

		/// <summary>
		/// Gets or sets the collection of <see cref="LegalEntityDocument"/>s
		/// associated with the current <see cref="LegalEntity"/>.
		/// </summary>
		public ICollection<LegalEntityDocument> Documents { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="SalesCustomer"/> associated with the 
		/// current <see cref="LegalEntity"/>.
		/// </summary>
		public Owner Owner { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Organisation"/> associated with the 
		/// current <see cref="LegalEntity"/>.
		/// </summary>
		public Organisation Organisation { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Individual"/> associated with the 
		/// current <see cref="LegalEntity"/>.
		/// </summary>
		public Individual Individual { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="OrganisationUnit"/> associated with the 
		/// current <see cref="LegalEntity"/>.
		/// </summary>
		public OrganisationUnit OrganisationUnit { get; set; }

		/// <summary>
		/// Gets or set when the current <see cref="LegalEntity"/>'s 
		/// KYC (Know Your Client) status will expire.
		/// </summary>
		public DateTime KYCValidUntil { get; set; }

		/// <summary>
		/// Gets or set the name of the person who verified the current 
		/// <see cref="LegalEntity"/>'s KYC (Know Your Client) status.
		/// </summary>
		public string KYCVerifiedBy { get; set; }

		/// <summary>
		/// Gets or set the <see cref="DateTime"/> the current 
		/// <see cref="LegalEntity"/>'s KYC (Know Your Client) status was verified on.
		/// </summary>
		public DateTime KYCVerifiedOn { get; set; }

		/// <summary>
		/// Gets or set when the current <see cref="LegalEntity"/>'s 
		/// FICA status will expire.
		/// </summary>
		public DateTime FICAValidUntil { get; set; }

		/// <summary>
		/// Gets or set the name of the person who verified the current 
		/// <see cref="LegalEntity"/>'s FICA status.
		/// </summary>
		public string FICAVerifiedBy { get; set; }

		/// <summary>
		/// Gets or set the <see cref="DateTime"/> the current 
		/// <see cref="LegalEntity"/>'s FICA status was verified on.
		/// </summary>
		public DateTime FICAVerifiedOn { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of an alternative legal entity that should be 
		/// invoiced by default.
		/// </summary>
		public long? InvoiceLegalEntityId { get; set; }

		/// <summary>
		/// Gets or sets an alternative legal entity that should be 
		/// invoiced by default.
		/// </summary>
		public LegalEntity InvoiceLegalEntity { get; set; }

		/// <summary>
		/// Gets or sets if the current legal entity's KYC details needs to be human verified.
		/// </summary>
		public bool KYCVerificationRequested { get; set; }

		/// <summary>
		/// Gets or sets if the current legal entity's KYC details needs to be human verified.
		/// </summary>
		public bool FICAVerificationRequested { get; set; }

		/// <summary>
		/// Gets or sets the flag indicating if integration to Sage failed.
		/// </summary>
		public bool HasIntegrationFailed { get; set; }

		/// <summary>
		/// Gets or sets the reason why the integration failed to Sage.
		/// </summary>
		public string IntegrationFailureReason { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new default instance of <see cref="LegalEntity"/>.
		/// </summary>
		public LegalEntity()
        {

        }

        #endregion
    }
}