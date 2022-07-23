using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlueMile.Data.Models
{ 
    /// <summary>
    /// <c>ContactDetailTypeEnum</c> defines the collection of available 
    /// <see cref="ContactDetailType"/>s.
    /// </summary>
    public enum ContactDetailTypeEnum : int
    {
        [Display(Name = "Email", Order = 1)]
        EmailAddress = 1,

        [Display(Name = "Mobile", Order = 2)]
        MobileNumber = 2,

        [Display(Name = "Phone", Order = 3)]
        Phone = 3,

        [Display(Name = "Fax", Order = 4)]
        Fax = 4,

        [Display(Name = "Web Site", Order = 5)]
        WebSite = 5,

        [Display(Name = "Other", Order = 6)]
        Other = 6
    }

    /// <summary>
    /// <c>ContactDetailType</c> defines the available types of contact details.
    /// </summary>
    public partial class ContactDetailType : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityContactDetails"/>s associated 
        /// with the current <see cref="ContactDetailType"/>.
        /// </summary>
        public ICollection<LegalEntityContactDetail> ContactDetails { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="ContactDetailType"/>.
        /// </summary>
        public ContactDetailType()
        {

        }

        #endregion
    }
}
