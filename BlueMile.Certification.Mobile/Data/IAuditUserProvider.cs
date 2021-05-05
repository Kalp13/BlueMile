using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data
{
	/// <summary>
	/// <c>IAuditUserProvider</c> provides access to the user or service attempting 
	/// to manipulate the underlying data for database auditing purposes.
	/// </summary>
	public interface IAuditUserProvider
	{
		/// <summary>
		/// Gets the name of the user or service attempting 
		/// to manipulate the underlying data for database auditing purposes.
		/// </summary>
		/// <returns>
		///		Returns the name of the user or service under the current 
		///		operational context.
		/// </returns>
		string GetCurrentUsername();
	}
}
