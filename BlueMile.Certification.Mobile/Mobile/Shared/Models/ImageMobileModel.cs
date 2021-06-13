using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class DocumentMobileModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string UniqueFileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public byte[] FileContent { get; set; }
    }
}
