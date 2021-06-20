using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueMile.Certification.Mobile.Data.Static
{
    public enum RequestStatesEnum
    {
        [Description("Requested")]
        Requested = 1,

        [Description("In Progress")]
        InProgress = 2,

        [Description("Rejected")]
        Rejected = 3,

        [Description("Approved")]
        Approved = 4,

        [Description("Provisioned")]
        Provisioned = 5,

        [Description("Completed")]
        Completed = 6,

        [Description("Cancelled")]
        Cancelled = 6,
    }
}
