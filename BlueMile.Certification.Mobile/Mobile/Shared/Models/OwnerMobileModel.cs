using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class OwnerMobileModel
    {
        #region Instance Fields

        public Guid Id { get; set; }

        public Guid SystemId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string Identification { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string Suburb { get; set; }

        public string Town { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public bool IsSynced { get; set; }

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }

        public ImageMobileModel IcasaPopPhoto { get; set; }

        public ImageMobileModel IdentificationDocument { get; set; }

        public ImageMobileModel SkippersLicenseImage { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="OwnerMobileModel"/>.
        /// </summary>
        public OwnerMobileModel()
        {

        }

        #endregion

        #region Class Methods

        public override string ToString()
        {
            var message = $"{this.Name} {this.Surname}\n" +
                          $"{this.Identification}\n" +
                          $"{this.Email}\n" +
                          $"{this.ContactNumber}\n" +
                          $"{this.SkippersLicenseNumber}\n" +
                          $"{this.VhfOperatorsLicense}";
            return message;
        }

        #endregion
    }
}