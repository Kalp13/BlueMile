using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class DocumentMobileModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public Guid SystemId { get; set; }

        public byte[] FileContent { get; set; }
    }
}
