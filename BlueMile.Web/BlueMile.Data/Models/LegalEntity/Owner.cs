using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    public class Owner : IBaseDbEntity
    {
		#region Instance Properties

		/// <summary>
		/// Gets or sets the unique <see cref="LegalEntity"/> identifier for the 
		/// current <see cref="SalesCustomer"/>.
		/// </summary>
		public long LegalEntityId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="LegalEntity"/> identifier for the 
		/// current <see cref="SalesCustomer"/>.
		/// </summary>
		public LegalEntity LegalEntity { get; set; }

		/// <summary>
		/// Gets or sets the auction buyer number for the current <see cref="SalesCustomer"/>.
		/// </summary>
		public string OwnerNumber { get; set; }

		/// <summary>
		/// Gets or sets the collection of <see cref="Sellable"/>s
		/// associated with the current <see cref="SalesCustomer"/>.
		/// </summary>
		public ICollection<Boat> Boats { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ApplicationUser"/> associated to the 
		/// current <see cref="SalesCustomer"/>.
		/// </summary>
		public ApplicationUser User { get; set; }

        #endregion

        #region Constructor

		/// <summary>
		/// Creates a new default instance of <see cref="Owner"/>.
		/// </summary>
		public Owner()
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
