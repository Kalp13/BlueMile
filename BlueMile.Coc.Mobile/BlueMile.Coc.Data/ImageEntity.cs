using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlueMile.Coc.Data
{
    public class ImageEntity
    {
        [PrimaryKey, Required]
        public Guid Id { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Indexed, Required]
        public string UniqueImageName { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        public string ImagePath { get; set; }
    }
}
