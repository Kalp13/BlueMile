using System.ComponentModel.DataAnnotations;

namespace BlueMile.Data.Models
{
    /// <summary>
    /// <c>GenderEnum</c> defines the collection of available 
    /// <see cref="Gender"/>s.
    /// </summary>
    public enum GenderEnum : int
    {
        [Display(Name = "Male", Order = 1)]
        Male = 1,

        [Display(Name = "Female", Order = 2)]
        Female = 2
    }

    /// <summary>
    /// <c>IdentificationType</c> defines the available types of genders.
    /// </summary>
    public class Gender : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Individual"/>s associated 
        /// with the current <see cref="Gender"/>.
        /// </summary>
        public ICollection<Individual> Individuals { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="Gender"/>.
        /// </summary>
        public Gender()
        {

        }

        #endregion
    }
}
