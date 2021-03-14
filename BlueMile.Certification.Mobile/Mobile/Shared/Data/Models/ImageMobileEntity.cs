using SQLite;
using System;

namespace BlueMile.Certification.Mobile.Data.Models
{
    public class ImageMobileEntity
    {
        [AutoIncrement, PrimaryKey]
        public long Id { get; set; }

        public Guid SystemId { get; set; }

        public string ImageName { get; set; }

        public string UniqueImageName { get; set; }

        public string FilePath { get; set; }
    }
}
