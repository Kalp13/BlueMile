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

        public CertificationRepository(ApplicationDbContext dbContext)
        {
            this.applicationDb = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #endregion

        #region ICertification Implementation

        #region Owner Methods

        public async Task<Guid> CreateOwner(OwnerModel entity)
        {
            var owner = OwnerHelper.ToCreateOwnerModel(entity);
            await this.applicationDb.Owners.AddAsync(owner);

            await this.applicationDb.SaveChangesAsync();

            return owner.Id;
        }

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

        public async Task<bool> DoesOwnerExist(Guid ownerId)
        {
            var owner = await this.applicationDb.Owners.FindAsync(ownerId);

            return owner != null;
        }

        public async Task<List<OwnerModel>> FindAllOwners()
        {
            var owners = this.applicationDb.Owners.Where(x => x.IsActive);

            return await owners.Select(y => OwnerHelper.ToApiOwnerModel(y)).ToListAsync();
        }

        public async Task<OwnerModel> FindOwnerById(Guid ownerId)
        {
            var owner = await this.applicationDb.Owners.FindAsync(ownerId);

            return OwnerHelper.ToApiOwnerModel(owner);
        }

        public async Task<bool> UpdateOwner(OwnerModel entity)
        {
            this.applicationDb.Owners.Update(OwnerHelper.ToUpdateOwnerModel(entity));

            return (await this.applicationDb.SaveChangesAsync()) > 0;
        }

        #endregion

        #region Boat Methods

        public async Task<Guid> CreateBoat(BoatModel entity)
        {
            var boat = BoatHelper.ToCreateBoatData(entity);
            await this.applicationDb.Boats.AddAsync(boat);

            await this.applicationDb.SaveChangesAsync();

            return boat.Id;
        }

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

        public async Task<bool> DoesBoatExist(Guid boatId)
        {
            var owner = await this.applicationDb.Boats.FindAsync(boatId);

            return owner != null;
        }

        public async Task<List<BoatModel>> FindAllBoats()
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).ToListAsync();
        }

        public async Task<List<BoatModel>> FindAllBoatsByOwnerId(Guid ownerId)
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive && x.Id == ownerId);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).ToListAsync();
        }

        public async Task<BoatModel> FindBoatById(Guid boatId)
        {
            var boats = this.applicationDb.Boats.Where(x => x.IsActive && x.Id == boatId);

            return await boats.Select(y => BoatHelper.ToApiBoatModel(y)).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBoat(BoatModel entity)
        {
            this.applicationDb.Boats.Update(BoatHelper.ToUpdateBoatData(entity));

            return (await this.applicationDb.SaveChangesAsync()) > 0;
        }

        #endregion

        #region Item Methods

        public Task<Guid> CreateItem(ItemModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(Guid entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesItemExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ItemModel>> FindAllItems()
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel> FindItemById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ItemModel>> FindItemsByBoatId(Guid boatId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItem(ItemModel entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Instance Fields

        private readonly ApplicationDbContext applicationDb;

        #endregion
    }
}
