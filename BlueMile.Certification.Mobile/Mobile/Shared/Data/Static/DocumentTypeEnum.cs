using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Static
{
	/// <summary>
	/// <c>DocumentTypeEnum</c> defines the collection of available 
	/// <see cref="DocumentType"/>s.
	/// </summary>
	public enum DocumentTypeEnum : int
	{
		[Description("Other")]
		Other = 1,

		[Description("Photo")]
		Photo = 2,

		[Description("Identification Document")]
		IdentificationDocument = 3,

		[Description("Proof Of Address")]
		ProofOfAddress = 4,

		[Description("Proof of Banking Details")]
		ProofOfBankDetails = 5,

		[Description("Proof Of Payment")]
		ProofOfPayment = 6,

		[Description("Passport")]
		Passport = 7,

		[Description("Boat Boyancy Certificate")]
		BoatBoyancyCertificate = 8,

		[Description("Tubbies Boyancy Certificate")]
		TubbiesBoyancyCertificate = 9,
	}
}
