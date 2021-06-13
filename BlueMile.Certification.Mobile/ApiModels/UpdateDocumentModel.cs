using System;

namespace BlueMile.Certification.Web.ApiModels
{
    public class UpdateLegalEntityDocumentModel
    {
        public Guid SystemId { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }

        public string DocumentType { get; set; }

        public byte[] FileContent { get; set; }
    }
}