using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlueMile.Data.Models.StaticData
{
    public enum RequestStateEnum : int
    {
        [Display(Name = "Requested", Order = 1)]
        Requested = 1,

        [Display(Name = "In Progress", Order = 2)]
        InProgress = 2,

        [Display(Name = "Rejected", Order = 3)]
        Rejected = 3,

        [Display(Name = "Approved", Order = 4)]
        Approved = 4,

        [Display(Name = "Requested", Order = 5)]
        Provisioned = 5,

        [Display(Name = "Requested", Order = 6)]
        Completed = 6,
    }

    /// <summary>
    /// <c>RequestState</c> defines the available states for a <c>CertificationRequest</c>.
    /// </summary>
    public partial class RequestState : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="RequestState"/>s associated 
        /// with the current <see cref="CertificationRequest"/>.
        /// </summary>
        public ICollection<CertificationRequest> Requests { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="RequestState"/>.
        /// </summary>
        public RequestState()
        {

        }

        #endregion
    }
}
