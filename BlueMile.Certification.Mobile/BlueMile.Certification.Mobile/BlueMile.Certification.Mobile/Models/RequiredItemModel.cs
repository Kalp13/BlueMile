using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Models
{
    public class RequiredItemModel
    {
        public long ItemType { get; set; }

        public int Quantity { get; set; }

        public bool RequirementFullfilled { get; set; }
    }
}
