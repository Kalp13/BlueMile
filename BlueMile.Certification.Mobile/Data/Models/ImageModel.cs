using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
    public class ImageModel
    {
        public Guid Id { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
