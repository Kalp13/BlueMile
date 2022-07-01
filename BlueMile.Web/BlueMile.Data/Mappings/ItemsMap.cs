using BlueMile.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMile.Data.Mappings
{
    internal sealed class ItemsMap : IEntityTypeConfiguration<Item>
    {
        #region Constructor

        public ItemsMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", "item");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Boat).WithMany().HasForeignKey(y => y.BoatId);
        }

        #endregion
    }

    internal sealed class ItemDocumentMap : IEntityTypeConfiguration<ItemDocument>
    {
        #region Constructors

        public ItemDocumentMap()
        {
        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<ItemDocument> builder)
        {
            builder.ToTable("ItemDocuments", "item");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Item).WithMany(x => x.Documents).HasForeignKey(x => x.ItemId).IsRequired();

        }

        #endregion
    }

    internal sealed class ItemTypeMap : IEntityTypeConfiguration<ItemType>
    {
        #region Constructors

        public ItemTypeMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.ToTable("ItemTypes", "item");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Items).WithOne(x => x.ItemType).HasForeignKey(x => x.ItemTypeId);
            var enumData = Enum.GetValues(typeof(ItemTypeEnum)).OfType<ItemTypeEnum>().Select(i => new ItemType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
            builder.HasData(enumData);
        }

        #endregion
    }
}
