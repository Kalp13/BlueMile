using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Data
{
	/// <summary>
	/// <c>IBaseDbEntity</c> defines and provides access to common database tracking 
	/// fields.
	/// </summary>
	public interface IBaseDbEntity
	{
		#region Instance Properties

		/// <summary>
		/// The <see cref="DateTime"/> the entity was created.
		/// </summary>
		DateTime CreatedOn { get; set; }

		/// <summary>
		/// The last <see cref="DateTime"/> the entity was modified.
		/// </summary>
		DateTime ModifiedOn { get; set; }

		/// <summary>
		/// Gets or sets the name of the logged in user who created the entity.
		/// </summary>
		string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the name of the logged in user who last modified the entity.
		/// </summary>
		string ModifiedBy { get; set; }

		/// <summary>
		/// Gets or sets the an indication of the current entity is regarded as active 
		/// in the database.
		/// </summary>
		bool IsActive { get; set; }

		#endregion
	}
}
