using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Data.Models.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueMile.Certification.Data.Mappings
{
    internal sealed class BoatsMap : IEntityTypeConfiguration<Boat>
    {
        #region Constructor

        public BoatsMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<Boat> builder)
        {
            builder.ToTable("Boats", "boat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x => x.Items).WithOne(x => x.Boat).HasForeignKey(x => x.BoatId);
        }

        #endregion
    }

    internal sealed class BoatCategoryMap : IEntityTypeConfiguration<BoatCategory>
    {
        #region Constructors

        public BoatCategoryMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<BoatCategory> builder)
        {
            builder.ToTable("BoatCategories", "boat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Boats).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
            var enumData = Enum.GetValues(typeof(BoatCategoryEnum)).OfType<BoatCategoryEnum>().Select(i => new BoatCategory() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
            builder.HasData(enumData);
        }

        #endregion
    }

    internal sealed class BoatDocumentMap : IEntityTypeConfiguration<BoatDocument>
    {
        #region Constructors

        public BoatDocumentMap()
        {
        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<BoatDocument> builder)
        {
            builder.ToTable("BoatDocuments", "boat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Boat).WithMany(x => x.Documents).HasForeignKey(x => x.BoatId).IsRequired();

        }

        #endregion
    }

    internal sealed class CertificationRequestMap : IEntityTypeConfiguration<CertificationRequest>
    {
        #region Constructors

        public CertificationRequestMap()
        {
        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<CertificationRequest> builder)
        {
            builder.ToTable("CertificationRequests", "boat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Boat).WithMany(x => x.CertificationRequests).HasForeignKey(x => x.BoatId).IsRequired();

        }

        #endregion
    }

    internal sealed class CertificationRequestStateMap : IEntityTypeConfiguration<RequestState>
    {
        #region Constructors

        public CertificationRequestStateMap()
        {

        }

        #endregion

        #region IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<RequestState> builder)
        {
            builder.ToTable("RequestStates", "boat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Requests).WithOne(x => x.RequestState).HasForeignKey(x => x.RequestStateId);
            var enumData = Enum.GetValues(typeof(RequestStateEnum)).OfType<RequestStateEnum>().Select(i => new RequestState() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
            builder.HasData(enumData);
        }

        #endregion
    }
}
