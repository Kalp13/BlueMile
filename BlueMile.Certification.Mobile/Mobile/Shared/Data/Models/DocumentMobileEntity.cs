using SQLite;
using System;

namespace BlueMile.Certification.Mobile.Data.Models
{
    public class DocumentMobileEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid SystemId { get; set; }

        public string FileType { get; set; }

        public string FileName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }
}
