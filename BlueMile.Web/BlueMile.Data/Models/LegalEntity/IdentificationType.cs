using System.ComponentModel.DataAnnotations;

namespace BlueMile.Data.Models
{
    /// <summary>
    /// <c>IdentificationTypeEnum</c> defines the collection of available 
    /// <see cref="IdentificationType"/>s.
    /// </summary>
    public enum IdentificationTypeEnum : int
    {
        [Display(Name = "South African", Order = 1)]
        SouthAfrican = 1,

        [Display(Name = "Foreigner", Order = 2)]
        Foreigner = 2,

        [Display(Name = "Refugee", Order = 3)]
        Refugee = 3,

        [Display(Name = "Permanent Resident", Order = 4)]
        PermanentResident = 4
    }

    /// <summary>
    /// <c>IdentificationType</c> defines the available identification types.
    /// </summary>
    public class IdentificationType : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Individual"/>s associated 
        /// with the current <see cref="IdentificationType"/>.
        /// </summary>
        public ICollection<Individual> Individuals { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="IdentificationType"/>.
        /// </summary>
        public IdentificationType()
        {

        }

        #endregion
    }
}
