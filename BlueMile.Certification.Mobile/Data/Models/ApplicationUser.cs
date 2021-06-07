using Microsoft.AspNetCore.Identity;
using System;

namespace BlueMile.Certification.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
	{
		public Guid? OwnerId { get; set; }

		public LegalEntity Owner { get; set; }

		public DateTime? LastLogin { get; set; }
	}
}
