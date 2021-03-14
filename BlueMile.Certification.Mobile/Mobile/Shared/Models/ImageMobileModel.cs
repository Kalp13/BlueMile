using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class ImageMobileModel
    {
        public long Id { get; set; }

        public Guid SystemId { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }
}
