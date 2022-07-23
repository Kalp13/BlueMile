using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    /// <summary>
    /// <c>LegalEntityRelationshipTypeEnum</c> defines the collection of available 
    /// <see cref="LegalEntityRelationshipType"/>s.
    /// </summary>
    public enum LegalEntityRelationshipTypeEnum : int
    {
        [Display(Name = "BoardMember", Order = 1)]
        BoardMember = 1,

        [Display(Name = "Branch", Order = 2)]
        Branch = 2,

        [Display(Name = "Buyer", Order = 3)]
        Buyer = 3,

        [Display(Name = "ContactPerson", Order = 4)]
        ContactPerson = 4,

        [Display(Name = "Director", Order = 5)]
        Director = 5,

        [Display(Name = "Division", Order = 6)]
        Division = 6,

        [Display(Name = "Employee", Order = 7)]
        Employee = 7,

        [Display(Name = "Owner", Order = 8)]
        Owner = 8,

        [Display(Name = "Partner", Order = 9)]
        Partner = 9,

        [Display(Name = "Share Holder", Order = 10)]
        ShareHolder = 10,

        [Display(Name = "Trustee", Order = 11)]
        Trustee = 11,
    }

    /// <summary>
    /// <c>LegalEntityRelationshipType</c> defines the available typed of types 
    /// <see cref="LegalEntityRelationships"/>.
    /// </summary>
    public class LegalEntityRelationshipType : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Organisation"/>s associated 
        /// with the current <see cref="SalesChannel"/>.
        /// </summary>
        public ICollection<LegalEntityRelationship> LegalEntityRelationships { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="LegalEntityRelationshipType"/>.
        /// </summary>
        public LegalEntityRelationshipType()
        {

        }

        #endregion
    }
}
