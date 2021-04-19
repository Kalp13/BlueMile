using System;

namespace BlueMile.Certification.Data.Models
{
    public class OwnerModel
    {
        public Guid Id { get; set; }

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

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
