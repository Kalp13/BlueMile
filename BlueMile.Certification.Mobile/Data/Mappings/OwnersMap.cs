using BlueMile.Certification.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueMile.Certification.Data.Mappings
{
	internal sealed class LegalEntityMap : IEntityTypeConfiguration<LegalEntity>
	{
		#region Constructors

		public LegalEntityMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntity> builder)
		{
			builder.ToTable("LegalEntities", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasMany(x => x.Addresses).WithOne(x => x.LegalEntity).HasForeignKey(x => x.LegalEntityId);
			builder.HasMany(x => x.ContactDetails).WithOne(x => x.LegalEntity).HasForeignKey(x => x.LegalEntityId);
			builder.HasOne(x => x.Owner).WithOne().HasForeignKey<IndividualOwner>(b => b.Id);
		}

		#endregion
	}

	internal sealed class LegalEntityContactDetailsMap : IEntityTypeConfiguration<LegalEntityContactDetail>
	{
		#region Constructors

		public LegalEntityContactDetailsMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntityContactDetail> builder)
		{
			builder.ToTable("LegalEntityContactDetails", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Value).IsRequired();
			builder.HasOne(x => x.LegalEntity).WithMany(x => x.ContactDetails).HasForeignKey(x => x.LegalEntityId).IsRequired();
			builder.HasOne(x => x.ContactDetailType).WithMany(x => x.ContactDetails).HasForeignKey(x => x.ContactDetailTypeId).IsRequired();

		}

		#endregion
	}

	internal sealed class IndividualOwnerMap : IEntityTypeConfiguration<IndividualOwner>
	{
		#region Constructors

		public IndividualOwnerMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<IndividualOwner> builder)
		{
			builder.ToTable("IndividualOwner", "leg");
			builder.Property(x => x.FirstName).IsRequired();
			builder.Property(x => x.LastName).IsRequired();
		}

		#endregion
	}

	internal sealed class ContactDetailTypeMap : IEntityTypeConfiguration<ContactDetailType>
	{
		#region Constructors

		public ContactDetailTypeMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<ContactDetailType> builder)
		{
			builder.ToTable("ContactDetailTypes", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.HasMany(x => x.ContactDetails).WithOne(x => x.ContactDetailType).HasForeignKey(x => x.ContactDetailTypeId);

			var enumData = Enum.GetValues(typeof(ContactDetailTypeEnum)).OfType<ContactDetailTypeEnum>().Select(i => new ContactDetailType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);

		}

		#endregion
	}

	internal sealed class LegalEntityDocumentMap : IEntityTypeConfiguration<LegalEntityDocument>
	{
		#region Constructors

		public LegalEntityDocumentMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntityDocument> builder)
		{
			builder.ToTable("LegalEntityDocuments", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasOne(x => x.LegalEntity).WithMany(x => x.Documents).HasForeignKey(x => x.LegalEntityId).IsRequired();

		}

		#endregion
	}

	internal sealed class DocumentTypeMap : IEntityTypeConfiguration<DocumentType>
	{
		#region Constructors

		public DocumentTypeMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<DocumentType> builder)
		{
			builder.ToTable("DocumentTypes", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.HasMany(x => x.OwnerDocuments).WithOne(x => x.DocumentType).HasForeignKey(x => x.DocumentTypeId);
			builder.HasMany(x => x.BoatDocuments).WithOne(x => x.DocumentType).HasForeignKey(x => x.DocumentTypeId);
			builder.HasMany(x => x.ItemDocuments).WithOne(x => x.DocumentType).HasForeignKey(x => x.DocumentTypeId);

			var enumData = Enum.GetValues(typeof(DocumentTypeEnum)).OfType<DocumentTypeEnum>().Select(i => new DocumentType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);
		}

		#endregion
	}
}