using BlueMile.Certification.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Mappings
{
    internal sealed class ItemsMap : IEntityTypeConfiguration<ItemModel>
    {
        #region Constructor

        public ItemsMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<ItemModel> builder)
        {
            builder.ToTable("ItemModel", "item");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Boat).WithMany().HasForeignKey(y => y.BoatId);
        }

        #endregion
    }
}
