using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
	/// <summary>
	/// <c>DocumentTypeEnum</c> defines the collection of available 
	/// <see cref="DocumentType"/>s.
	/// </summary>
	public enum DocumentTypeEnum : int
	{

		[Display(Name = "Other", Order = 1)]
		Other = 1,

		[Display(Name = "Photo", Order = 2)]
		Photo = 2,

		[Display(Name = "Identification Document", Order = 3)]
		IdentificationDocument = 3,

		[Display(Name = "Proof Of Address", Order = 4)]
		ProofOfAddress = 4,

		[Display(Name = "Proof Of Bank Details", Order = 5)]
		ProofOfBankDetails = 5,

		[Display(Name = "Proof Of Payment", Order = 6)]
		ProofOfPayment = 6,

		[Display(Name = "Passport", Order = 7)]
		Passport = 7,
	}



	/// <summary>
	/// <c>DocumentType</c> defines the available types of documents.
	/// </summary>
	public partial class DocumentType : BaseEnumTable
	{
		#region Instance Properties

		/// <summary>
		/// Gets or sets the collection of <see cref="LegalEntityDocument"/>s associated 
		/// with the current <see cref="DocumentType"/>.
		/// </summary>
		public ICollection<Document> Documents { get; set; }


		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new default <see cref="DocumentType"/>.
		/// </summary>
		public DocumentType()
		{

		}

		#endregion
	}
}
