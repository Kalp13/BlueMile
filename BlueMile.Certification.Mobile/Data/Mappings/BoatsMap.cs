using BlueMile.Certification.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Mappings
{
    internal sealed class BoatsMap : IEntityTypeConfiguration<BoatModel>
    {
        #region Constructor

        public BoatsMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<BoatModel> builder)
        {
            builder.ToTable("BoatModel", "boat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x => x.Items).WithOne(x => x.Boat).HasForeignKey(x => x.BoatId);
        }

        #endregion
    }
}
