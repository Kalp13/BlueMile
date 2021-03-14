using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class ImageModel
    {
        public Guid SystemId { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }

    public class CreateImageModel
    {
        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }

    public class UpdateImageModel
    {
        public Guid SystemId { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }
}