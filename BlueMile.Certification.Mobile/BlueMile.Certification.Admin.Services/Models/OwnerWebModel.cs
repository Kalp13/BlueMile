using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Services.Models
{
    public class OwnerWebModel
    {
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

        /// <summary>
        /// Gets or sets the <c>VHFOperatorsLicense</c> of this Owner.
        /// </summary>
        public string VhfOperatorsLicense { get; set; }

        /// <summary>
        /// Gets or sets the <c>SkippersLicenseNumber</c> of this Owner.
        /// </summary>
        public string SkippersLicenseNumber { get; set; }
    }
}
