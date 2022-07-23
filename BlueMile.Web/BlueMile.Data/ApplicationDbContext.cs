﻿using BlueMile.Certification.Data;
using BlueMile.Data.Mappings;
using BlueMile.Data.Models;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlueMile.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IDataProtectionKeyContext, IPersistedGrantDbContext
    {
		#region Legal Entities

		public virtual DbSet<LegalEntity> LegalEnitites { get; set; }

		public virtual DbSet<Individual> Individuals { get; set; }

		public virtual DbSet<Organisation> Organisations { get; set; }

		public virtual DbSet<OrganisationUnit> OrganisationUnits { get; set; }

		public virtual DbSet<OrganisationType> OrganisationTypes { get; set; }

		public virtual DbSet<LegalEntityDocument> LegalEntityDocuments { get; set; }
		
		public virtual DbSet<LegalEntityAddress> LegalEntityAddresses { get; set; }

		public virtual DbSet<LegalEntityRelationshipState> LegalEntityRelationshipStates { get; set; }

		public virtual DbSet<LegalEntityRelationshipType> LegalEntityRelationshipTypes { get; set; }

		public virtual DbSet<ContactDetailType> ContactDetailTypes { get; set; }

		public virtual DbSet<LegalEntityContactDetail> LegalEntityContactDetails { get; set; }

		public virtual DbSet<LegalEntityRelationship> LegalEntityRelationships { get; set; }

		public virtual DbSet<Owner> Owners { get; set; }

		public virtual DbSet<Gender> Genders { get; set; }

		public virtual DbSet<Salutation> Salutations { get; set; }

		public virtual DbSet<IdentificationType> IdentificationTypes { get; set; }

		public virtual DbSet<DocumentType> DocumentTypes { get; set; }

		#endregion

		#region Boats

		public virtual DbSet<Boat> Boats { get; set; }

		public virtual DbSet<BoatDocument> BoatDocuments { get; set; }

		public virtual DbSet<BoatCategory> BoatCategories { get; set; }

		public virtual DbSet<CertificationRequest> CertificationRequests { get; set; }

		public virtual DbSet<RequestState> RequestStates { get; set; }

		#endregion

		#region Items

		public virtual DbSet<Item> Items { get; set; }

		public virtual DbSet<ItemType> ItemTypes { get; set; }

		public virtual DbSet<ItemDocument> ItemDocuments { get; set; }

		public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuditUserProvider auditUserProvider)
			: base(options)
		{
			// Validate Parameters.
			if (auditUserProvider == null)
			{
				throw new ArgumentNullException("auditUserProvider");
			}

			this.auditUserProvider = auditUserProvider;
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			this.CaptureUserAuditDetails();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public override int SaveChanges()
		{
			this.CaptureUserAuditDetails();
			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new ApplicationUserMap());

			builder.ApplyConfiguration(new LegalEntityMap());
			builder.ApplyConfiguration(new LegalEntityRelationshipTypeMap());
			builder.ApplyConfiguration(new LegalEntityRelationshipStateMap());
			builder.ApplyConfiguration(new LegalEntityRelationshipMap());
			builder.ApplyConfiguration(new LegalEntityContactDetailsMap());
			builder.ApplyConfiguration(new LegalEntityAddressMap());
			builder.ApplyConfiguration(new ContactDetailTypeMap());
			builder.ApplyConfiguration(new OrganisationTypeMap());
			builder.ApplyConfiguration(new OrganisationMap());
			builder.ApplyConfiguration(new OrganisationUnitMap());
			builder.ApplyConfiguration(new GenderMap());
			builder.ApplyConfiguration(new SalutationMap());
			builder.ApplyConfiguration(new IdentificationTypeMap());
			builder.ApplyConfiguration(new IndividualMap());
			builder.ApplyConfiguration(new DocumentTypeMap());
			builder.ApplyConfiguration(new LegalEntityDocumentMap());
			builder.ApplyConfiguration(new OwnerMap());

			builder.ApplyConfiguration(new BoatsMap());
			builder.ApplyConfiguration(new BoatDocumentMap());
			builder.ApplyConfiguration(new BoatCategoryMap());
			builder.ApplyConfiguration(new CertificationRequestMap());
			builder.ApplyConfiguration(new CertificationRequestStateMap());

			builder.ApplyConfiguration(new ItemsMap());
			builder.ApplyConfiguration(new ItemDocumentMap());
			builder.ApplyConfiguration(new ItemTypeMap());

			this.UseUtcDates(builder);

			this.SeedRoles(builder);
			this.SeedUsers(builder);

			base.OnModelCreating(builder);
		}

		#region Instance Methods

		private void SeedRoles(ModelBuilder builder)
		{
			var applicationRoles =
				new ApplicationRole[] {
					new ApplicationRole() { Id = ApplicationRoleIdentifiers.Administrator, Name = "Administrator", NormalizedName = ApplicationRoleNames.Administrator ,ConcurrencyStamp =  "0e30899f-95ce-4c84-9d91-8f77388cd430"}             ,
					new ApplicationRole() { Id = ApplicationRoleIdentifiers.AdminUser, Name = "Admin User", NormalizedName = ApplicationRoleNames.AdminUser, ConcurrencyStamp =  "7ECA10E7-4830-42D5-93FE-480770102868"},
					new ApplicationRole() { Id = ApplicationRoleIdentifiers.OwnerUser, Name = "Customer User", NormalizedName = ApplicationRoleNames.OwnerUser, ConcurrencyStamp =  "5783D52D-375D-46D5-8ECD-C9DF27EE02FE"}
				};

			builder.Entity<ApplicationRole>().HasData(applicationRoles);
		}

		private void SeedUsers(ModelBuilder builder)
		{
			Guid adminId = Guid.Parse("8F0FA92C-6B34-4C88-8504-BE6B6A4EC52F");
			string adminUserName = "admin@bluemile.co.za";

			var hasher = new PasswordHasher<ApplicationUser>();
			builder.Entity<ApplicationUser>().HasData(
				new ApplicationUser
				{
					Id = adminId,
					UserName = adminUserName,
					NormalizedUserName = adminUserName.ToUpperInvariant(),
					PasswordHash = "AQAAAAEAACcQAAAAEMKUjLkruKeo4D1h/N75pZefSVjt1o5CaojVGuO3iT/H+wF0KyGKS8Vdi1k8yGkSvg==",
					SecurityStamp = "4NQQFF7WPUI3TKAKKO3IONTXBLZYVJ4F",
					ConcurrencyStamp = "31f53ff4-a200-42d5-a103-a08f1ce9b6a8",
					Email = adminUserName,
					NormalizedEmail = adminUserName.ToUpperInvariant(),
					EmailConfirmed = true
				});

			builder.Entity<IdentityUserRole<Guid>>().HasData(
				new IdentityUserRole<Guid>()
				{
					UserId = adminId,
					RoleId = ApplicationRoleIdentifiers.AdminUser
				});

			builder.Entity<IdentityUserRole<Guid>>().HasData(
				new IdentityUserRole<Guid>()
				{
					UserId = adminId,
					RoleId = ApplicationRoleIdentifiers.Administrator
				});
		}

		private void UseUtcDates(ModelBuilder builder)
		{
			var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
				v => v.ToUniversalTime(),
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				foreach (var property in entityType.GetProperties())
				{
					if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
						property.SetValueConverter(dateTimeConverter);
				}
			}
		}

		private void CaptureUserAuditDetails()
		{
			var modified =
				this.ChangeTracker
					.Entries()
					.Where(x =>
						x.State == EntityState.Modified ||
						x.State == EntityState.Deleted)
					.Where(x => x.Entity is IBaseDbEntity);

			var created =
				this.ChangeTracker
					.Entries()
					.Where(x => x.State == EntityState.Added)
					.Where(x => x.Entity is IBaseDbEntity);

			var updatingUser = this.auditUserProvider.GetCurrentUsername();

			foreach (var item in modified)
			{
				item.CurrentValues[nameof(IBaseDbEntity.ModifiedOn)] = DateTime.Now;
				item.CurrentValues[nameof(IBaseDbEntity.ModifiedBy)] = updatingUser;
			}

			foreach (var item in created)
			{
				item.CurrentValues[nameof(IBaseDbEntity.ModifiedOn)] = DateTime.Now;
				item.CurrentValues[nameof(IBaseDbEntity.CreatedOn)] = DateTime.Now;
				item.CurrentValues[nameof(IBaseDbEntity.CreatedBy)] = updatingUser;
				item.CurrentValues[nameof(IBaseDbEntity.ModifiedBy)] = updatingUser;
				item.CurrentValues[nameof(IBaseDbEntity.IsActive)] = true;
			}
		}

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        private IAuditUserProvider auditUserProvider;
	}
}
