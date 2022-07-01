using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMile.Data.Mappings
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
