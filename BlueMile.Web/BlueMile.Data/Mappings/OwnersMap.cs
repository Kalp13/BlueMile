using BlueMile.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueMile.Data.Mappings
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
			builder.HasMany(x => x.Children).WithOne(x => x.Child).HasForeignKey(x => x.ChildId);
			builder.HasMany(x => x.Parents).WithOne(x => x.Parent).HasForeignKey(x => x.ParentId);
			builder.HasMany(x => x.ContactDetails).WithOne(x => x.LegalEntity).HasForeignKey(x => x.LegalEntityId);
			builder.HasOne(x => x.Owner).WithOne(x => x.LegalEntity).HasForeignKey<LegalEntity>(b => b.Id);
			builder.HasOne(x => x.Organisation).WithOne().HasForeignKey<Organisation>(b => b.Id);
			builder.HasOne(x => x.Individual).WithOne().HasForeignKey<Individual>(b => b.Id);
			builder.HasOne(x => x.OrganisationUnit).WithOne().HasForeignKey<OrganisationUnit>(b => b.Id);


			builder.HasOne(x => x.InvoiceLegalEntity).WithMany().HasForeignKey(b => b.InvoiceLegalEntityId).IsRequired(false);
		}

		#endregion
	}

	internal sealed class OwnerMap : IEntityTypeConfiguration<Owner>
	{
		#region Constructors

		public OwnerMap()
		{

		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<Owner> builder)
		{
			builder.ToTable("SalesCustomers", "sales");
			builder.HasKey(x => x.LegalEntityId);
			builder.HasOne(x => x.LegalEntity).WithOne(x => x.Owner).HasForeignKey<LegalEntity>(b => b.Id).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(x => x.Boats).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);
			builder.HasOne(x => x.User).WithOne(x => x.Owner).HasForeignKey<ApplicationUser>(x => x.OwnerId).IsRequired(false);
			builder.Property(x => x.OwnerNumber).IsRequired();
			builder.HasIndex(x => x.OwnerNumber).IsUnique();
		}

		#endregion
	}

	internal sealed class LegalEntityRelationshipTypeMap : IEntityTypeConfiguration<LegalEntityRelationshipType>
	{
		#region Constructors

		public LegalEntityRelationshipTypeMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntityRelationshipType> builder)
		{
			builder.ToTable("LegalEntityRelationshipTypes", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasMany(x => x.LegalEntityRelationships).WithOne(x => x.RelationshipType).HasForeignKey(x => x.RelationshipTypeId);
			var enumData =
				Enum.GetValues(typeof(LegalEntityRelationshipTypeEnum))
					.OfType<LegalEntityRelationshipTypeEnum>()
					.Select(i => new LegalEntityRelationshipType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);
		}

		#endregion
	}

	internal sealed class LegalEntityRelationshipStateMap : IEntityTypeConfiguration<LegalEntityRelationshipState>
	{
		#region Constructor

		public LegalEntityRelationshipStateMap()
		{

		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntityRelationshipState> builder)
		{
			builder.ToTable("LegalEntityRelationshipStates", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasMany(x => x.LegalEntityRelationships).WithOne(x => x.ApprovalState).HasForeignKey(x => x.RelationshipTypeId);
			var enumData =
				Enum.GetValues(typeof(ApprovalStateEnum))
					.OfType<ApprovalStateEnum>()
					.Select(i => new LegalEntityRelationshipState() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);
		}

		#endregion
	}

	internal sealed class LegalEntityRelationshipMap : IEntityTypeConfiguration<LegalEntityRelationship>
	{
		#region Constructors

		public LegalEntityRelationshipMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntityRelationship> builder)
		{
			builder.ToTable("LegalEntityRelationships", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasOne(x => x.Parent).WithMany(x => x.Parents).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(x => x.Child).WithMany(x => x.Children).HasForeignKey(x => x.ChildId).OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(x => x.RelationshipType).WithMany(x => x.LegalEntityRelationships).HasForeignKey(x => x.RelationshipTypeId);
			builder.HasOne(x => x.ApprovalState).WithMany(x => x.LegalEntityRelationships).HasForeignKey(x => x.ApprovalStateId);
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

	internal sealed class LegalEntityAddressMap : IEntityTypeConfiguration<LegalEntityAddress>
	{
		#region Constructors

		public LegalEntityAddressMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<LegalEntityAddress> builder)
		{
			builder.ToTable("LegalEntityAddress", "leg");
			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.LegalEntity).WithMany(x => x.Addresses).HasForeignKey(x => x.LegalEntityId).IsRequired();
			builder.Property(x => x.Longitude).HasColumnType("decimal(18, 14)");
			builder.Property(x => x.Latitude).HasColumnType("decimal(18, 14)");
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

	internal sealed class OrganisationTypeMap : IEntityTypeConfiguration<OrganisationType>
	{
		#region Constructors

		public OrganisationTypeMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<OrganisationType> builder)
		{
			builder.ToTable("OrganisationTypes", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.HasMany(x => x.Organisations).WithOne(x => x.OrganisationType).HasForeignKey(x => x.OrganisationTypeId);
			builder.HasMany(x => x.OrganisationUnits).WithOne(x => x.OrganisationType).HasForeignKey(x => x.OrganisationTypeId);

			var enumData = Enum.GetValues(typeof(OrganisationTypeEnum)).OfType<OrganisationTypeEnum>().Select(i => new OrganisationType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);
		}

		#endregion
	}

	internal sealed class OrganisationMap : IEntityTypeConfiguration<Organisation>
	{
		#region Constructors

		public OrganisationMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<Organisation> builder)
		{
			builder.ToTable("Organisations", "leg");
			builder.Property(x => x.Name).IsRequired();
			builder.HasOne(x => x.OrganisationType).WithMany(x => x.Organisations).HasForeignKey(x => x.OrganisationTypeId);
		}

		#endregion
	}

	internal sealed class OrganisationUnitMap : IEntityTypeConfiguration<OrganisationUnit>
	{
		#region Constructors

		public OrganisationUnitMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<OrganisationUnit> builder)
		{
			builder.ToTable("OrganisationUnits", "leg");
			builder.Property(x => x.Name).IsRequired();
			builder.HasOne(x => x.OrganisationType).WithMany(x => x.OrganisationUnits).HasForeignKey(x => x.OrganisationTypeId);
		}

		#endregion
	}

	internal sealed class GenderMap : IEntityTypeConfiguration<Gender>
	{
		#region Constructors

		public GenderMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<Gender> builder)
		{
			builder.ToTable("Genders", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.HasMany(x => x.Individuals).WithOne(x => x.Gender).HasForeignKey(x => x.GenderId);

			var enumData = Enum.GetValues(typeof(GenderEnum)).OfType<GenderEnum>().Select(i => new Gender() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);

		}

		#endregion
	}

	internal sealed class SalutationMap : IEntityTypeConfiguration<Salutation>
	{
		#region Constructors

		public SalutationMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<Salutation> builder)
		{
			builder.ToTable("Salutations", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.HasMany(x => x.Individuals).WithOne(x => x.Salutation).HasForeignKey(x => x.SalutationId);

			var enumData = Enum.GetValues(typeof(SalutationEnum)).OfType<SalutationEnum>().Select(i => new Salutation() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);

			#endregion
		}
	}

	internal sealed class IdentificationTypeMap : IEntityTypeConfiguration<IdentificationType>
	{
		#region Constructors

		public IdentificationTypeMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<IdentificationType> builder)
		{
			builder.ToTable("IdentificationTypes", "leg");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.HasMany(x => x.Individuals).WithOne(x => x.IdentificationType).HasForeignKey(x => x.IdentificationTypeId);

			var enumData = Enum.GetValues(typeof(IdentificationTypeEnum)).OfType<IdentificationTypeEnum>().Select(i => new IdentificationType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
			builder.HasData(enumData);

		}

		#endregion
	}

	internal sealed class IndividualMap : IEntityTypeConfiguration<Individual>
	{
		#region Constructors

		public IndividualMap()
		{
		}

		#endregion

		#region IEntityTypeConfiguration Implementation

		public void Configure(EntityTypeBuilder<Individual> builder)
		{
			builder.ToTable("Individuals", "leg");
			builder.HasOne(x => x.Salutation).WithMany(x => x.Individuals).HasForeignKey(x => x.SalutationId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
			builder.HasOne(x => x.Gender).WithMany(x => x.Individuals).HasForeignKey(x => x.GenderId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
			builder.HasOne(x => x.IdentificationType).WithMany(x => x.Individuals).HasForeignKey(x => x.IdentificationTypeId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
			builder.Property(x => x.DateOfBirth).IsRequired(false);
			builder.Property(x => x.FirstName).IsRequired();
			builder.Property(x => x.LastName).IsRequired();
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
			builder.HasMany(x => x.LegalEntityDocuments).WithOne(x => x.DocumentType).HasForeignKey(x => x.DocumentTypeId);

			var enumData = Enum.GetValues(typeof(DocumentTypeEnum)).OfType<DocumentTypeEnum>().Select(i => new DocumentType() { Id = (int)i, Name = i.ToDisplayName(), Order = i.ToOrder(), IsActive = i.IsActive() });
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
			builder.Property(x => x.MimeType).IsRequired();
			builder.Property(x => x.FileName).IsRequired();
			builder.Property(x => x.UniqueFileName).IsRequired();
			builder.HasOne(x => x.LegalEntity).WithMany(x => x.Documents).HasForeignKey(x => x.LegalEntityId).IsRequired();
			builder.HasOne(x => x.DocumentType).WithMany(x => x.LegalEntityDocuments).HasForeignKey(x => x.DocumentTypeId).IsRequired();

		}

		#endregion
	}
}