using BlueMile.Certification.WASM.Server.Data;
using BlueMile.Certification.WASM.Server.Models.Helpers;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WASM.Server.Services
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
            await this.applicationDb.Owners.AddAsync(owner);

            await this.applicationDb.SaveChangesAsync();

            return owner.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteOwner(Guid ownerId)
        {
            if (await this.DoesOwnerExist(ownerId))
            {
                var owner = await this.FindOwnerById(ownerId);
                this.applicationDb.Owners.Remove(OwnerHelper.ToOwnerDataModel(owner));

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
            var owner = await this.applicationDb.Owners.FindAsync(ownerId);

            return owner != null;
        }

        /// <inheritdoc/>
        public async Task<List<OwnerModel>> FindAllOwners()
        {
            var owners = this.applicationDb.Owners.Where(x => x.IsActive);

            return await owners.Select(y => OwnerHelper.ToApiOwnerModel(y)).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> FindOwnerById(Guid ownerId)
        {
            var owner = await this.applicationDb.Owners.FindAsync(ownerId);

            return OwnerHelper.ToApiOwnerModel(owner);
        }

        /// <inheritdoc/>
        public Task<bool> UpdateOwner(UpdateOwnerModel entity)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();
            var owner = OwnerHelper.ToUpdateOwnerModel(entity);
            this.applicationDb.Owners.Update(owner);

            var result = this.applicationDb.SaveChanges();

            completionSource.SetResult(result > 0);

            return completionSource.Task;
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
        public Task<bool> UpdateBoat(UpdateBoatModel entity)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            var boat = BoatHelper.ToUpdateBoatData(entity);
            this.applicationDb.Boats.Update(boat);

            var result = this.applicationDb.SaveChanges();

            completionSource.SetResult(result > 0);

            return completionSource.Task;
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
        public Task<bool> UpdateItem(UpdateItemModel entity)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            var item = ItemHelper.ToUpdateItemModel(entity);
            this.applicationDb.Items.Update(item);

            var result = this.applicationDb.SaveChanges();

            completionSource.SetResult(result > 0);

            return completionSource.Task;
        }

        #endregion

        #endregion

        #region Instance Fields

        private readonly ApplicationDbContext applicationDb;

        #endregion
    }
}
