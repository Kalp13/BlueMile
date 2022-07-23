using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    public class Individual : LegalEntity, IBaseDbEntity
    {
		#region Instance Properties

		/// <summary>
		/// Gets or sets the initials of the current <see cref="Individual"/>.
		/// </summary>
		public string Initials { get; set; }

		/// <summary>
		/// Gets or sets the first name of the current <see cref="Individual"/>.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the surname of the current <see cref="Individual"/>.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the alternative name of the current <see cref="Individual"/>.
		/// </summary>
		public string KnownAs { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="Salutation"/> identifier for the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public int? SalutationId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Salutation"/> for the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public Salutation Salutation { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="Gender"/> identifier for the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public int? GenderId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Gender"/> for the current <see cref="Individual"/>.
		/// </summary>
		public Gender Gender { get; set; }

		/// <summary>
		/// Gets or sets the nationality of the current <see cref="Individual"/>.
		/// </summary>
		public string Nationality { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DateTime"/> date of birth for the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public DateTime? DateOfBirth { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="IdentificationType"/> identifier provided 
		/// by the current <see cref="Individual"/>.
		/// </summary>
		public int? IdentificationTypeId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="IdentificationType"/> provided 
		/// by the current <see cref="Individual"/>.
		/// </summary>
		public IdentificationType IdentificationType { get; set; }

		/// <summary>
		/// Gets or sets the identification number provided by the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public string IdentificationNumber { get; set; }

		/// <summary>
		/// Gets or sets the traffic register identification number provided by the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public string TrafficRegisterNumber { get; set; }

		/// <summary>
		/// Gets or sets the passport number provided by the 
		/// current <see cref="Individual"/>.
		/// </summary>
		public string PassportNumber { get; set; }

		/// <summary>
		/// Gets or sets the issuing country passport of the current <see cref="Individual"/>.
		/// </summary>
		public string PassportCountry { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new default instance of <see cref="Individual"/>.
		/// </summary>
		public Individual()
        {

        }

        #endregion

        #region IBaseDbEntity Implementation

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; }

        #endregion
    }
}
