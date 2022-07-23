using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    /// <summary>
	/// <c>OrganisationTypeEnum</c> defines the collection of available 
	/// <see cref="OrganisationType"/>s.
	/// </summary>
	public enum OrganisationTypeEnum : int
    {
        [Display(Name = "Branch", Order = 1)]
        Branch = 1,

        [Display(Name = "Closed Corporation", Order = 2)]
        ClosedCorporation = 2,

        [Display(Name = "Co-Operative", Order = 3)]
        CoOperative = 3,

        [Display(Name = "Department", Order = 4)]
        Department = 4,

        [Display(Name = "Division", Order = 5)]
        Division = 5,

        [Display(Name = "External Company", Order = 6)]
        ExternalCompany = 6,

        [Display(Name = "Government Department", Order = 7)]
        GovernmentDepartment = 7,

        [Display(Name = "Non-Profit Company", Order = 8)]
        NonProfitCompany = 8,

        [Display(Name = "Non RSA Company", Order = 9)]
        NonRsaCompany = 9,

        [Display(Name = "Partnership", Order = 10)]
        Partnership = 10,

        [Display(Name = "Personal Liability", Order = 11)]
        PersonalLiability = 11,

        [Display(Name = "Private Company", Order = 12)]
        PrivateCompany = 12,

        [Display(Name = "Public Company", Order = 13)]
        PublicCompany = 13,

        [Display(Name = "Sole Proprietor", Order = 14)]
        SoleProprietor = 14,

        [Display(Name = "State Owned Company", Order = 15)]
        StateownedCompany = 15,

        [Display(Name = "Trust", Order = 16)]
        Trust = 16,
    }

    /// <summary>
    /// <c>OrganisationType</c> defines a type of organisations.
    /// </summary>
    public class OrganisationType : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Organisation"/>s associated 
        /// with the current <see cref="OrganisationType"/>.
        /// </summary>
        public ICollection<Organisation> Organisations { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="OrganisationUnit"/>s associated 
        /// with the current <see cref="OrganisationType"/>.
        /// </summary>
        public ICollection<OrganisationUnit> OrganisationUnits { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="OrganisationType"/>.
        /// </summary>
        public OrganisationType()
        {

        }

        #endregion
    }
}
