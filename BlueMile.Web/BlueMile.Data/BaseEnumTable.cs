using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Data
{
	public class BaseEnumTable : IBaseDbEntity
	{
		/// <summary>
		/// Gets or sets the current unique identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the current entity value.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the display order of the current entity value.
		/// </summary>
		public int Order { get; set; }

		#region IBaseDbEntity Implementation

		public DateTime CreatedOn { get; set; }

		public DateTime ModifiedOn { get; set; }

		public string CreatedBy { get; set; }

		public string ModifiedBy { get; set; }

		public bool IsActive { get; set; }

		#endregion
	}
}
