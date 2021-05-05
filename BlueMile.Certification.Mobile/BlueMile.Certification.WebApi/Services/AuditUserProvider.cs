using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Services
{
	public class AuditUserProvider : IAuditUserProvider
	{
		public AuditUserProvider(AuthenticationStateProvider authenticationStateProvider)
		{
			// Validate Parameters.            
			if (authenticationStateProvider == null)
			{
				throw new ArgumentNullException("authenticationStateProvider");
			}
			this.authenticationStateProvider = authenticationStateProvider;

		}

		public string GetCurrentUsername()
		{
			try
			{
				var user = this.authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
				return user?.Identity.Name;
			}
			catch (InvalidOperationException)
			{
				// Continue				
				return null;
			}
		}

		private AuthenticationStateProvider authenticationStateProvider;
	}
}
