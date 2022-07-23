using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    public enum ApprovalStateEnum
    {
        [Display(Name = "Pending", Order = 1)]
        Pending = 1,

        [Display(Name = "Approved", Order = 2)]
        Approved = 2,

        [Display(Name = "Rejected", Order = 3)]
        Rejected = 3,

        [Display(Name = "Cancelled", Order = 4)]
        Cancelled = 4,
    }

    /// <summary>
    /// <c>LegalEntityRelationshipType</c> defines the available typed of types 
    /// <see cref="LegalEntityRelationships"/>.
    /// </summary>
    public class LegalEntityRelationshipState : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityRelationship"/>s associated 
        /// with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<LegalEntityRelationship> LegalEntityRelationships { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="LegalEntityRelationshipState"/>.
        /// </summary>
        public LegalEntityRelationshipState()
        {

        }

        #endregion
    }
}
