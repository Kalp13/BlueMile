using BlueMile.Certification.Data;
using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Data;
using BlueMile.Certification.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Services
{
    public class CertificationRepository : ICertificationRepository
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="CertificationRepository"/>.
        /// </summary>
        /// <param name="dbContext"></param>
        public CertificationRepository(ApplicationDbContext dbContext)
        {
            this.applicationDb = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #endregion

        #region ICertification Implementation

        #region Owner Methods

        /// <inheritdoc/>
        public async Task<Guid> CreateOwner(CreateOwnerModel entity)
        {
            var owner = OwnerHelper.ToCreateOwnerModel(entity);
            await this.applicationDb.IndividualsOwners.AddAsync(owner);
            
            if (entity.IdentificationDocument != null)
            {
                var idDoc = DocumentHelper.ToCreateDocumentModel(entity.IdentificationDocument);
                await this.applicationDb.LegalEntityDocuments.AddAsync(idDoc);
            }
            if (entity.SkippersLicenseImage != null)
            {
                var skipDoc = DocumentHelper.ToCreateDocumentModel(entity.SkippersLicenseImage);
                await this.applicationDb.LegalEntityDocuments.AddAsync(skipDoc);
            }
            if (entity.IcasaPopPhoto != null)
            {
                var icasaDoc = DocumentHelper.ToCreateDocumentModel(entity.IcasaPopPhoto);
                await this.applicationDb.LegalEntityDocuments.AddAsync(icasaDoc);
            }

            await this.applicationDb.SaveChangesAsync();

            await this.CreateAddress(OwnerHelper.ToCreateAddressModel(entity), owner.Id);
            await this.CreateContactDetails(entity, owner.Id);

            return owner.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteOwner(Guid ownerId)
        {
            if (await this.DoesOwnerExist(ownerId))
            {
                var owner = await this.FindOwnerById(ownerId);
                this.applicationDb.IndividualsOwners.Remove(OwnerHelper.ToOwnerDataModel(owner));

                return (await this.applicationDb.SaveChangesAsync()) > 0;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesOwnerExist(Guid ownerId)
        {
            var owner = await this.applicationDb.IndividualsOwners.FindAsync(ownerId);

            return owner != null;
        }

        /// <inheritdoc/>
        public async Task<List<OwnerModel>> FindAllOwners(FindOwnerModel findOwnerModel)
        {
            if (findOwnerModel == null)
            {
                throw new ArgumentNullException(nameof(findOwnerModel));
            }

            var owners = this.applicationDb.IndividualsOwners.Where(x => x.IsActive);

            if (!String.IsNullOrWhiteSpace(findOwnerModel.SearchTerm))
            {
                owners.Where(x => x.FirstName.Contains(findOwnerModel.SearchTerm) ||
                                  x.LastName.Contains(findOwnerModel.SearchTerm) ||
                                  x.Identification.Contains(findOwnerModel.SearchTerm));
            }

            if (findOwnerModel.OwnerId.HasValue && findOwnerModel.OwnerId.Value != Guid.Empty)
            {
                owners = owners.Where(x => x.Id == findOwnerModel.OwnerId);
            }

            return await owners.Select(y => OwnerHelper.ToApiOwnerModel(y)).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> FindOwnerByUsername(string username)
        {
            var owners = this.applicationDb.IndividualsOwners.Where(x => x.IsActive);

            return await owners.Select(y => OwnerHelper.ToApiOwnerModel(y)).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> FindOwnerById(Guid ownerId)
        {
            var owner = await this.applicationDb.IndividualsOwners.FindAsync(ownerId);

            return OwnerHelper.ToApiOwnerModel(owner);
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateOwner(UpdateOwnerModel entity)
        {
            var owner = OwnerHelper.ToUpdateOwnerModel(entity);
            this.applicationDb.IndividualsOwners.Update(owner);

            if (entity.IdentificationDocument != null)
            {
                var idDoc = DocumentHelper.ToCreateDocumentModel(entity.IdentificationDocument);
                await this.applicationDb.LegalEntityDocuments.AddAsync(idDoc);
            }
            if (entity.SkippersLicenseImage != null)
            {
                var skipDoc = DocumentHelper.ToCreateDocumentModel(entity.SkippersLicenseImage);
                await this.applicationDb.LegalEntityDocuments.AddAsync(skipDoc);
            }
            if (entity.IcasaPopPhoto != null)
            {
                var icasaDoc = DocumentHelper.ToCreateDocumentModel(entity.IcasaPopPhoto);
                await this.applicationDb.LegalEntityDocuments.AddAsync(icasaDoc);
            }

            var result = await this.applicationDb.SaveChangesAsync();

            await this.CreateAddress(OwnerHelper.ToUpdateAddressModel(entity), owner.Id);
            await this.UpdateContactDetails(entity, owner.Id);

            if (result > 0)
            {
                return entity.SystemId;
            }
            else
            {
                throw new ArgumentException(nameof(entity));
            }
        }

        #endregion

        #region Boat Methods

        /// <inheritdoc/>
        public async Task<Guid> CreateBoat(CreateBoatModel entity)
        {
            var boat = BoatHelper.ToCreateBoatData(entity);
            await this.applicationDb.Boats.AddAsync(boat);

            await this.applicationDb.SaveChangesAsync();

            return boat.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBoat(Guid boatId)
        {
            if (await this.DoesBoatExist(boatId))
            {
                var boat = await this.FindBoatById(boatId);
                this.applicationDb.Boats.Remove(BoatHelper.ToBoatDataModel(boat));

                return (await this.applicationDb.SaveChangesAsync()) > 0;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesBoatExist(Guid boatId)
        {
            var boat = await this.applicationDb.Boats.FindAsync(boatId);

            return boat != null;
        }

        /// <inheritdoc/>
        public async Task<List<BoatModel>> FindAllBoats()
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<BoatModel>> FindAllBoatsByOwnerId(Guid ownerId)
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive && x.Id == ownerId);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<BoatModel> FindBoatById(Guid boatId)
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive && x.Id == boatId);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateBoat(UpdateBoatModel entity)
        {
            var boat = BoatHelper.ToUpdateBoatData(entity);
            this.applicationDb.Boats.Update(boat);

            var result = await this.applicationDb.SaveChangesAsync();

            if (result > 0)
            {
                return entity.SystemId;
            }
            else
            {
                throw new ArgumentException(nameof(entity));
            }
        }

        #endregion

        #region Item Methods

        /// <inheritdoc/>
        public async Task<Guid> CreateItem(CreateItemModel entity)
        {
            var item = ItemHelper.ToCreateItemModel(entity);
            await this.applicationDb.Items.AddAsync(item);

            await this.applicationDb.SaveChangesAsync();

            return item.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid itemId)
        {
            if (await this.DoesItemExist(itemId))
            {
                var item = await this.FindItemById(itemId);
                this.applicationDb.Items.Remove(ItemHelper.ToItemDataModel(item));

                return (await this.applicationDb.SaveChangesAsync()) > 0;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesItemExist(Guid id)
        {
            var item = await this.applicationDb.Items.FindAsync(id);

            return item != null;
        }

        /// <inheritdoc/>
        public async Task<List<ItemModel>> FindAllItems()
        {
            var items = await this.applicationDb.Items.Where(x => x.IsActive).ToListAsync();

            return items.Select(x => ItemHelper.ToItemApiModel(x)).ToList();
        }

        /// <inheritdoc/>
        public async Task<ItemModel> FindItemById(Guid id)
        {
            var item = await this.applicationDb.Items.FindAsync(id);

            return ItemHelper.ToItemApiModel(item);
        }

        /// <inheritdoc/>
        public async Task<List<ItemModel>> FindItemsByBoatId(Guid boatId)
        {
            var items = await this.applicationDb.Items.Where(x => x.IsActive && x.BoatId == boatId).ToListAsync();

            return items.Select(x => ItemHelper.ToItemApiModel(x)).ToList();
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateItem(UpdateItemModel entity)
        {
            var item = ItemHelper.ToUpdateItemModel(entity);
            this.applicationDb.Items.Update(item);

            var result = await this.applicationDb.SaveChangesAsync();

            if (result > 0)
            {
                return entity.SystemId;
            }
            else
            {
                throw new ArgumentException(nameof(entity));
            }
        }

        #endregion

        #endregion

        #region Class Methods

        private async Task CreateContactDetails(CreateOwnerModel entity, Guid id)
        {
            if (!String.IsNullOrWhiteSpace(entity.ContactNumber))
            {
                var phoneContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.MobileNumber);
                await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                {
                    ContactDetailTypeId = phoneContact.Id,
                    LegalEntityId = id,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedBy = entity.ContactNumber,
                    CreatedOn = DateTime.Now,
                    Value = entity.ContactNumber,
                });
            }

            if (!String.IsNullOrWhiteSpace(entity.Email))
            {
                var emailContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.EmailAddress);
                await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                {
                    ContactDetailTypeId = emailContact.Id,
                    LegalEntityId = id,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedBy = entity.Email,
                    CreatedOn = DateTime.Now,
                    Value = entity.ContactNumber,
                });
            }

            await this.applicationDb.SaveChangesAsync();
        }

        private async Task UpdateContactDetails(UpdateOwnerModel entity, Guid id)
        {
            if (!String.IsNullOrWhiteSpace(entity.ContactNumber))
            {
                var phoneContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.MobileNumber);
                await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                {
                    ContactDetailTypeId = phoneContact.Id,
                    LegalEntityId = id,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedBy = entity.Email,
                    CreatedOn = DateTime.Now,
                    Value = entity.ContactNumber,
                });
            }

            if (!String.IsNullOrWhiteSpace(entity.Email))
            {
                var emailContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.EmailAddress);
                await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                {
                    ContactDetailTypeId = emailContact.Id,
                    LegalEntityId = id,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedBy = entity.Email,
                    CreatedOn = DateTime.Now,
                    Value = entity.ContactNumber,
                });
            }

            await this.applicationDb.SaveChangesAsync();
        }

        private async Task CreateAddress(LegalEntityAddress address, Guid id)
        {
            if (!String.IsNullOrWhiteSpace(address.StreetNumber) &&
                !String.IsNullOrWhiteSpace(address.StreetName) &&
                !String.IsNullOrWhiteSpace(address.Town) &&
                !String.IsNullOrWhiteSpace(address.Province))
            {
                address.LegalEntityId = id;
                await this.applicationDb.LegalEntityAddress.AddAsync(address);
            }

            await this.applicationDb.SaveChangesAsync();
        }

        #endregion

        #region Instance Fields

        private readonly ApplicationDbContext applicationDb;

        #endregion
    }
}
