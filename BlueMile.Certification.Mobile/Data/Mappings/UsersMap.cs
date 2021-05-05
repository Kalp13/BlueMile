using BlueMile.Certification.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Mappings
{
	internal sealed class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
	{
		#region Constructors

		public ApplicationUserMap()
		{

		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId).IsRequired(false);
		}

		#endregion
	}
}
