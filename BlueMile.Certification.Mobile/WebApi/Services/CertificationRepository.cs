using BlueMile.Certification.Data;
using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Services
{
    /// <summary>
    /// Contains the implementation off all the <see cref="ICertificationRepository"/> 
    /// CRUD methods for <c>Owners</c>, <c>Boats</c> and <c>Items</c>.
    /// </summary>
    public class CertificationRepository : ICertificationRepository
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="CertificationRepository"/>.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="hostEnvironment"></param>
        public CertificationRepository(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            this.webHostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            this.applicationDb = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            this.baseImagePath = Path.Combine(webHostEnvironment.WebRootPath, "images");
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
                var idDoc = OwnerHelper.ToCreateDocumentModel(entity.IdentificationDocument);
                await this.applicationDb.LegalEntityDocuments.AddAsync(idDoc);
                await this.UploadOwnerDocumentAsync(entity.IdentificationDocument);
            }
            if (entity.SkippersLicenseImage != null)
            {
                var skipDoc = OwnerHelper.ToCreateDocumentModel(entity.SkippersLicenseImage);
                await this.applicationDb.LegalEntityDocuments.AddAsync(skipDoc);
                await this.UploadOwnerDocumentAsync(entity.SkippersLicenseImage);
            }
            if (entity.IcasaPopPhoto != null)
            {
                var icasaDoc = OwnerHelper.ToCreateDocumentModel(entity.IcasaPopPhoto);
                await this.applicationDb.LegalEntityDocuments.AddAsync(icasaDoc);
                await this.UploadOwnerDocumentAsync(entity.IcasaPopPhoto);
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
                var owner = await this.applicationDb.IndividualsOwners.FirstOrDefaultAsync();
                this.applicationDb.IndividualsOwners.Remove(owner);

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

            var owners = this.applicationDb.IndividualsOwners
                .Include("Addresses")
                .Include("ContactDetails")
                .Include("Documents")
                .Where(x => x.IsActive);

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

            return await owners.Select(y => OwnerHelper.ToApiOwnerModel(y, y.Addresses.FirstOrDefault(), 
                                                                           y.ContactDetails.Where(z => z.LegalEntityId == y.Id).ToArray(), 
                                                                           y.Documents.Where(x => x.LegalEntityId == y.Id).ToArray())).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> FindOwnerByUsername(string username)
        {
            var user = await this.applicationDb.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var owner = await this.applicationDb.IndividualsOwners.FirstOrDefaultAsync(x => x.Id == user.OwnerId);
            var contactDetails = await this.applicationDb.LegalEntityContactDetails.Where(x => x.LegalEntityId == owner.Id).OrderByDescending(x => x.CreatedOn).ToArrayAsync();
            var address = await this.applicationDb.LegalEntityAddress.Where(x => x.LegalEntityId == owner.Id).OrderByDescending(y => y.CreatedOn).FirstOrDefaultAsync();
            var documents = await this.applicationDb.LegalEntityDocuments.Where(x => x.LegalEntityId == owner.Id).OrderByDescending(x => x.CreatedOn).ToArrayAsync();

            var mappedOwner = OwnerHelper.ToApiOwnerModel(owner, address, contactDetails, documents);

            return mappedOwner;
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> FindOwnerById(Guid ownerId)
        {
            var owner = await this.applicationDb.IndividualsOwners.FindAsync(ownerId);
            var contactDetails = await this.applicationDb.LegalEntityContactDetails.Where(x => x.LegalEntityId == ownerId).OrderByDescending(x => x.CreatedOn).ToArrayAsync();
            var address = await this.applicationDb.LegalEntityAddress.Where(x => x.LegalEntityId == ownerId).OrderByDescending(y => y.CreatedOn).FirstOrDefaultAsync();
            var documents = await this.applicationDb.LegalEntityDocuments.Where(x => x.LegalEntityId == owner.Id).OrderByDescending(x => x.CreatedOn).ToArrayAsync();

            var mappedOwner = OwnerHelper.ToApiOwnerModel(owner, address, contactDetails, documents);

            return mappedOwner;
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateOwner(UpdateOwnerModel entity)
        {
            try
            {
                var owner = OwnerHelper.ToUpdateOwnerModel(entity);
                var existingOwner = await this.applicationDb.IndividualsOwners.FirstOrDefaultAsync(x => x.Id == entity.Id);
                existingOwner.FirstName = owner.FirstName;
                existingOwner.Identification = owner.Identification;
                existingOwner.LastName = owner.LastName;
                existingOwner.SkippersLicenseNumber = owner.SkippersLicenseNumber;
                existingOwner.VhfOperatorsLicense = owner.VhfOperatorsLicense;

                var result = await this.applicationDb.SaveChangesAsync();

                if (entity.IdentificationDocument != null)
                {
                    await this.UploadOwnerDocumentAsync(entity.IdentificationDocument);
                }
                if (entity.SkippersLicenseImage != null)
                {
                    await this.UploadOwnerDocumentAsync(entity.SkippersLicenseImage);
                }
                if (entity.IcasaPopPhoto != null)
                {
                    await this.UploadOwnerDocumentAsync(entity.IcasaPopPhoto);
                }

                await this.CreateAddress(OwnerHelper.ToUpdateAddressModel(entity, owner.Id), owner.Id);

                await this.UpdateContactDetails(entity, owner.Id);

                return owner.Id;
            }
            catch (Exception)
            {
                throw;
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

            if (entity.BoyancyCertificateImage != null)
            {
                await this.UploadBoatDocumentAsync(entity.BoyancyCertificateImage);
            }

            if (entity.TubbiesCertificateImage != null)
            {
                await this.UploadBoatDocumentAsync(entity.TubbiesCertificateImage);
            }

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
        public async Task<List<BoatModel>> FindAllBoats(FindBoatsModel model)
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<BoatModel>> FindAllBoatsByOwnerId(Guid ownerId)
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive && x.OwnerId == ownerId);

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
            var existingBoat = await this.applicationDb.Boats.FirstOrDefaultAsync(x => x.Id == entity.Id);
            var boat = BoatHelper.ToUpdateBoatData(entity);
            existingBoat.BoyancyCertificateNumber = boat.BoyancyCertificateNumber;
            existingBoat.CategoryId = boat.CategoryId;
            existingBoat.IsJetski = boat.IsJetski;
            existingBoat.Name = boat.Name;
            existingBoat.RegisteredNumber = boat.RegisteredNumber;
            existingBoat.TubbiesCertificateNumber = boat.TubbiesCertificateNumber;

            await this.applicationDb.SaveChangesAsync();

            if (entity.BoyancyCertificateImage != null)
            {
                await this.UploadBoatDocumentAsync(entity.BoyancyCertificateImage);
            }

            if (entity.TubbiesCertificateImage != null)
            {
                await this.UploadBoatDocumentAsync(entity.TubbiesCertificateImage);
            }

            return entity.Id;
        }

        #endregion

        #region Item Methods

        /// <inheritdoc/>
        public async Task<Guid> CreateItem(CreateItemModel entity)
        {
            var item = ItemHelper.ToCreateItemModel(entity);
            await this.applicationDb.Items.AddAsync(item);

            await this.applicationDb.SaveChangesAsync();

            if (entity.ItemImage != null)
            {
                await this.UploadItemDocumentAsync(entity.ItemImage);
            }

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
        public async Task<List<ItemModel>> FindAllItems(FindItemsModel model)
        {
            var items = await this.applicationDb.Items.Where(x => x.IsActive).ToListAsync();

            return items.Select(x => ItemHelper.ToItemApiModel(x)).ToList();
        }

        /// <inheritdoc/>
        public async Task<ItemModel> FindItemById(Guid itemId)
        {
            var item = await this.applicationDb.Items.FirstOrDefaultAsync(x => x.Id == itemId);

            if (item != null)
            {
                return ItemHelper.ToItemApiModel(item);
            }
            else
            {
                return null;
            }
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
            var existingItem = await this.applicationDb.Items.FirstOrDefaultAsync(x => x.Id == entity.Id);
            var item = ItemHelper.ToUpdateItemModel(entity);

            existingItem.SerialNumber = item.SerialNumber;
            existingItem.CapturedDate = item.CapturedDate;
            existingItem.Description = item.Description;
            existingItem.ExpiryDate = item.ExpiryDate;

            await this.applicationDb.SaveChangesAsync();

            if (entity.ItemImage != null)
            {
                await this.UploadItemDocumentAsync(entity.ItemImage);
            }

            return entity.Id;
        }

        #endregion

        #endregion

        #region Class Methods

        private async Task CreateContactDetails(CreateOwnerModel entity, Guid ownerId)
        {
            if (!String.IsNullOrWhiteSpace(entity.ContactNumber))
            {
                var phoneContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.MobileNumber);
                await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                {
                    ContactDetailTypeId = phoneContact.Id,
                    LegalEntityId = ownerId,
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
                    LegalEntityId = ownerId,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreatedBy = entity.Email,
                    CreatedOn = DateTime.Now,
                    Value = entity.Email,
                });
            }

            await this.applicationDb.SaveChangesAsync();
        }

        private async Task UpdateContactDetails(UpdateOwnerModel entity, Guid ownerId)
        {
            if (!String.IsNullOrWhiteSpace(entity.ContactNumber))
            {
                var phoneContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.MobileNumber);
                var existingContact = await this.applicationDb.LegalEntityContactDetails.Where(x => x.LegalEntityId == ownerId).FirstOrDefaultAsync(y => y.ContactDetailTypeId == phoneContact.Id);
                if (existingContact == null)
                {
                    await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                    {
                        ContactDetailTypeId = phoneContact.Id,
                        LegalEntityId = ownerId,
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        CreatedBy = entity.Email,
                        CreatedOn = DateTime.Now,
                        Value = entity.ContactNumber,
                    });
                }
                else
                {
                    existingContact.Value = entity.ContactNumber;
                }
            }

            if (!String.IsNullOrWhiteSpace(entity.Email))
            {
                var emailContact = await this.applicationDb.ContactDetailTypes.FirstOrDefaultAsync(x => x.Id == (int)ContactDetailTypeEnum.EmailAddress);
                var existingContact = await this.applicationDb.LegalEntityContactDetails.Where(x => x.LegalEntityId == ownerId).FirstOrDefaultAsync(y => y.ContactDetailTypeId == emailContact.Id);
                if (existingContact == null)
                {
                    await this.applicationDb.LegalEntityContactDetails.AddAsync(new LegalEntityContactDetail()
                    {
                        ContactDetailTypeId = emailContact.Id,
                        LegalEntityId = ownerId,
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        CreatedBy = entity.Email,
                        CreatedOn = DateTime.Now,
                        Value = entity.ContactNumber,
                    });
                }
                else
                {
                    existingContact.Value = entity.Email;
                }
                
            }

            await this.applicationDb.SaveChangesAsync();
        }

        private async Task CreateAddress(LegalEntityAddress address, Guid ownerId)
        {
            if (!String.IsNullOrWhiteSpace(address.StreetNumber) &&
                !String.IsNullOrWhiteSpace(address.StreetName) &&
                !String.IsNullOrWhiteSpace(address.Town) &&
                !String.IsNullOrWhiteSpace(address.Province))
            {
                var existingAddress = await this.applicationDb.LegalEntityAddress.Where(x => x.LegalEntityId == ownerId).OrderByDescending(y => y.CreatedOn).FirstOrDefaultAsync();
                if (this.DidAddressChange(address, existingAddress))
                {
                    await this.applicationDb.LegalEntityAddress.AddAsync(address);
                }
                else
                {
                    existingAddress.UnitNumber = address.UnitNumber;
                    existingAddress.ComplexName = address.ComplexName;
                    existingAddress.StreetNumber = address.StreetNumber;
                    existingAddress.StreetName = address.StreetName;
                    existingAddress.Suburb = address.Suburb;
                    existingAddress.Town = address.Town;
                    existingAddress.Province = address.Province;
                    existingAddress.Country = address.Country;
                    existingAddress.PostalCode = address.PostalCode;
                }
            }

            await this.applicationDb.SaveChangesAsync();
        }

        /// <summary>
        /// Validates whether or not the address changed in a significant manner at all.
        /// </summary>
        /// <param name="address">
        ///     The new address from the service.
        /// </param>
        /// <param name="existingAddress">
        ///     The existing address on the system.
        /// </param>
        /// <returns>
        ///     Returns a boolean flag indicating if the address changed.
        /// </returns>
        private bool DidAddressChange(LegalEntityAddress address, LegalEntityAddress existingAddress)
        {
            return address?.StreetNumber.ToLower(CultureInfo.InvariantCulture) != existingAddress?.StreetNumber.ToLower(CultureInfo.InvariantCulture) ||
                   address?.StreetName.ToLower(CultureInfo.InvariantCulture) != existingAddress?.StreetName.ToLower(CultureInfo.InvariantCulture) ||
                   address?.Suburb.ToLower(CultureInfo.InvariantCulture) != existingAddress?.Suburb.ToLower(CultureInfo.InvariantCulture) ||
                   address?.Town.ToLower(CultureInfo.InvariantCulture) != existingAddress?.Town.ToLower(CultureInfo.InvariantCulture) ||
                   address?.Province.ToLower(CultureInfo.InvariantCulture) != existingAddress?.Province.ToLower(CultureInfo.InvariantCulture) ||
                   address?.Country.ToLower(CultureInfo.InvariantCulture) != existingAddress?.Country.ToLower(CultureInfo.InvariantCulture);
        }

        private async Task UploadOwnerDocumentAsync(OwnerDocumentModel model)
        {
            string uploadsFolder = $@"{this.baseImagePath}\owners\{model.LegalEntityId}";
            model.FilePath = Path.Combine(uploadsFolder, model.UniqueFileName);

            var ownerDoc = OwnerHelper.ToUpdateDocumentModel(model);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var existingDoc = await this.applicationDb.LegalEntityDocuments.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existingDoc == null)
            {
                await this.applicationDb.LegalEntityDocuments.AddAsync(ownerDoc);
            }
            else
            {
                existingDoc.FileName = ownerDoc.FileName;
                existingDoc.FilePath = ownerDoc.FilePath;
                existingDoc.UniqueFileName = ownerDoc.UniqueFileName;
            }

            await File.WriteAllBytesAsync(model.FilePath, model.FileContent);

            await this.applicationDb.SaveChangesAsync();

            return;
        }

        private async Task UploadBoatDocumentAsync(BoatDocumentModel model)
        {
            string uploadsFolder = $@"{this.baseImagePath}\boats\{model.BoatId}";
            model.FilePath = Path.Combine(uploadsFolder, model.UniqueFileName);
            var boatDoc = BoatHelper.ToUpdateDocumentModel(model);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var existingDoc = await this.applicationDb.BoatDocuments.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existingDoc == null)
            {
                await this.applicationDb.BoatDocuments.AddAsync(boatDoc);
            }
            else
            {
                existingDoc.FileName = boatDoc.FileName;
                existingDoc.FilePath = boatDoc.FilePath;
                existingDoc.UniqueFileName = boatDoc.UniqueFileName;
            }

            await File.WriteAllBytesAsync(model.FilePath, model.FileContent);

            await this.applicationDb.SaveChangesAsync();

            return;
        }

        private async Task UploadItemDocumentAsync(ItemDocumentModel model)
        {
            string uploadsFolder = $@"{this.baseImagePath}\items\{model.ItemId}";
            model.FilePath = Path.Combine(uploadsFolder, model.UniqueFileName);
            var itemDoc = ItemHelper.ToUpdateDocumentModel(model);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var existingDoc = await this.applicationDb.BoatDocuments.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existingDoc == null)
            {
                await this.applicationDb.ItemDocuments.AddAsync(itemDoc);
            }
            else
            {
                existingDoc.FileName = itemDoc.FileName;
                existingDoc.FilePath = itemDoc.FilePath;
                existingDoc.UniqueFileName = itemDoc.UniqueFileName;
            }

            await File.WriteAllBytesAsync(model.FilePath, model.FileContent);

            await this.applicationDb.SaveChangesAsync();

            return;
        }

        #endregion

        #region Instance Fields

        private readonly ApplicationDbContext applicationDb;

        private readonly IWebHostEnvironment webHostEnvironment;

        private string baseImagePath;

        #endregion
    }
}
