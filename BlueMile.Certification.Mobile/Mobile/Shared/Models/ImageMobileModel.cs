using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class ImageMobileModel
    {
        public Guid Id { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }
}
