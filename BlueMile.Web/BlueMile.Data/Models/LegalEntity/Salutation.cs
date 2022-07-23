using System.ComponentModel.DataAnnotations;

namespace BlueMile.Data.Models
{
    /// <summary>
    /// <c>SalutationEnum</c> defines the collection of available 
    /// <see cref="Salutation"/>s.
    /// </summary>
    public enum SalutationEnum : int
    {
        [Display(Name = "Mr", Order = 1)]
        Mr = 1,

        [Display(Name = "Miss", Order = 2)]
        Miss = 2,

        [Display(Name = "Mrs", Order = 3)]
        Mrs = 3,

        [Display(Name = "Dr", Order = 4)]
        Dr = 4,

        [Display(Name = "Rev", Order = 5)]
        Rev = 5,

        [Display(Name = "Prof", Order = 6)]
        Prof = 6,

        [Display(Name = "Sir", Order = 7)]
        Sir = 7
    }

    /// <summary>
    /// <c>Salutation</c> defines a types of <see cref="Individual"/> 
    /// salutations or titles.
    /// </summary>
    public class Salutation : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Individual"/>s associated 
        /// with the current <see cref="Salutation"/>.
        /// </summary>
        public ICollection<Individual> Individuals { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="Salutation"/>.
        /// </summary>
        public Salutation()
        {

        }

        #endregion
    }
}
