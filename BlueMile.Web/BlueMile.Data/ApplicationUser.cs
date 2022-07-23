﻿using BlueMile.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BlueMile.Data
{
    public class ApplicationUser : IdentityUser<Guid>
	{
		public Guid? OwnerId { get; set; }

		public Owner Owner { get; set; }

		public DateTime? LastLogin { get; set; }
	}
}
