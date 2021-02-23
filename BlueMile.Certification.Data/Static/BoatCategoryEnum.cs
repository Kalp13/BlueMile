using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueMile.Certification.Data.Static
{
    public enum BoatCategoryEnum
    {
        [Description("Category C")]
        C = 1,
        [Description("Category D")]
        D,
        [Description("Category E")]
        E,
        [Description("Category Z")]
        Z
    }
}
