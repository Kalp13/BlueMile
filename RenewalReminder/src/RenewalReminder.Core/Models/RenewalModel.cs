using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RenewalReminder.Core.Models
{
    public class RenewalModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
