using BlueMile.Certification.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Mappings
{
    internal sealed class OwnersMap : IEntityTypeConfiguration<OwnerModel>
	{
		#region Constructors

		public OwnersMap()
		{

		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<OwnerModel> builder)
		{
			builder.HasMany(x => x.Boats).WithOne(y => y.Owner).HasForeignKey(x => x.OwnerId).IsRequired(false);
		}

		#endregion
	}
}
