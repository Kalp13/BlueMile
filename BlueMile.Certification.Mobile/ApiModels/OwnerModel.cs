using System;

namespace BlueMile.Certification.Web.ApiModels
{
    public class OwnerModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the email of the owner.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the owner.
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the unit number of the complex or estate.
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the complex or estate.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Gets or sets the street number of the address.
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the street.
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating if this owner has been synchronized to the server.
        /// </summary>
        public bool IsSynced { get; set; }

        public OwnerDocumentModel IcasaPopPhoto { get; set; }

        public OwnerDocumentModel IdentificationDocument { get; set; }

        public OwnerDocumentModel SkippersLicenseImage { get; set; }
    }

    public class CreateOwnerModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the Owner to create.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the contact email of the owner.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the owner.
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the unit number of the complex or estate.
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the complex or estate.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Gets or sets the street number of the address.
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the street.
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        public OwnerDocumentModel IcasaPopPhoto { get; set; }

        public OwnerDocumentModel IdentificationDocument { get; set; }

        public OwnerDocumentModel SkippersLicenseImage { get; set; }
    }

    public class UpdateOwnerModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the Owner to create.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the contact email of the owner.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the owner.
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the unit number of the complex or estate.
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the complex or estate.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Gets or sets the street number of the address.
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the street.
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        public OwnerDocumentModel IcasaPopPhoto { get; set; }

        public OwnerDocumentModel IdentificationDocument { get; set; }

        public OwnerDocumentModel SkippersLicenseImage { get; set; }
    }
}
