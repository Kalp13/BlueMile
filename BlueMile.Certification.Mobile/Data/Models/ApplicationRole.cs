using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
	public class ApplicationRole : IdentityRole<Guid>
	{

	}

	public static class ApplicationRoleIdentifiers
	{
		public static readonly Guid Administrator = Guid.Parse("2E5D4077-A9C7-4E28-9E55-FDF6B41F6302");
		public static readonly Guid AdminUser = Guid.Parse("22E2592F-CF6D-4A6E-85AA-E853AB28F336");
		public static readonly Guid OwnerUser = Guid.Parse("B2CD6DD3-9905-42C9-996F-D8FCBF5B0554");
	}


	public static class ApplicationRoleNames
	{
		public const string Administrator = "Administrator";
		public const string AdminUser = "Admin User";
		public const string OwnerUser = "Owner User";
	}
}
