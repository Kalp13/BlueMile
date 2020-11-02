using BlueMile.Coc.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Coc.Mobile.Models
{
    public class ItemRquirementModel
    {
        public ItemTypeStaticEntity ItemType { get; set; }

        public int Quantity { get; set; }

        public bool RequirementFullfilled { get; set; }
    }
}
