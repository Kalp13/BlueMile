using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class DocumentModel
    {
        public Guid SystemId { get; set; }

        public string FileName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }

        public string DocumentType { get; set; }

        public byte[] FileContent { get; set; }
    }

    public class CreateImageModel
    {
        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }

        public string DocumentType { get; set; }

        public byte[] FileContent { get; set; }
    }

    public class UpdateImageModel
    {
        public Guid SystemId { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }

        public string DocumentType { get; set; }

        public byte[] FileContent { get; set; }
    }
}