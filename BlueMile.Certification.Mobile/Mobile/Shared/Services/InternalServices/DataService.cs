using BlueMile.Certification.Mobile.Data.Helpers;
using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Helpers;
using BlueMile.Certification.Mobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public class DataService : IDataService
    {
        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="DataService"/>.
        /// </summary>
        public DataService()
        {
            this.dataConnection = this.InitializeDBConnection();
            this.CreateTables();
        }

        #endregion

        #region IDataService Implementation

        #region Owner Data Methods

        /// <inheritdoc/>
        public async Task<List<OwnerMobileModel>> FindOwnersAsync()
        {
            try
            {
                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var response = await this.dataConnection.Table<OwnerMobileEntity>().ToListAsync().ConfigureAwait(false);

                return response.Select(x => OwnerHelper.ToOwnerModel(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateNewOwnerAsync(OwnerMobileModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }
                var ownerEntity = OwnerHelper.ToOwnerDataEntity(owner);
                var idDocEntity = ImageHelper.ToImageDataEntity(owner.IdentificationDocument);
                var skippersDocument = ImageHelper.ToImageDataEntity(owner.SkippersLicenseImage);
                var icasaPopPhoto = ImageHelper.ToImageDataEntity(owner.IcasaPopPhoto);

                var response = await this.dataConnection.InsertAsync(ownerEntity, typeof(OwnerMobileEntity)).ConfigureAwait(false);
                var idResponse = await this.dataConnection.InsertAsync(idDocEntity, typeof(DocumentMobileEntity)).ConfigureAwait(false);
                var skippersResponse = await this.dataConnection.InsertAsync(skippersDocument, typeof(DocumentMobileEntity)).ConfigureAwait(false);
                var icasaResponse = await this.dataConnection.InsertAsync(icasaPopPhoto, typeof(DocumentMobileEntity)).ConfigureAwait(false);

                return ownerEntity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerMobileModel> FindOwnerBySystemIdAsync(Guid systemId)
        {
            try
            {
                if (systemId == null || systemId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(systemId));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var owner = await this.dataConnection.Table<OwnerMobileEntity>().FirstOrDefaultAsync(x => x.SystemId == systemId).ConfigureAwait(false);

                if (owner != null)
                {
                    var result = OwnerHelper.ToOwnerModel(owner);
                    if (owner.IdentificationDocumentId.HasValue)
                    {
                        result.IdentificationDocument = await this.FindImageByIdAsync(owner.IdentificationDocumentId.Value);
                    }
                    
                    if (owner.SkippersLicenseImageId.HasValue)
                    {
                        result.SkippersLicenseImage = await this.FindImageByIdAsync(owner.SkippersLicenseImageId.Value);
                    }
                    
                    if (owner.IcasaPopImageId.HasValue)
                    {
                        result.IcasaPopPhoto = await this.FindImageByIdAsync(owner.IcasaPopImageId.Value);
                    }

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerMobileModel> FindOwnerByIdAsync(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var owner = await this.dataConnection.FindAsync<OwnerMobileEntity>(id).ConfigureAwait(false);

                if (owner != null)
                {
                    return OwnerHelper.ToOwnerModel(owner);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateOwnerAsync(OwnerMobileModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var ownerEntity = OwnerHelper.ToOwnerDataEntity(owner);
                var idDocEntity = ImageHelper.ToImageDataEntity(owner.IdentificationDocument);
                var skippersDocument = ImageHelper.ToImageDataEntity(owner.SkippersLicenseImage);
                var icasaPopPhoto = ImageHelper.ToImageDataEntity(owner.IcasaPopPhoto);

                var response = await this.dataConnection.UpdateAsync(ownerEntity, typeof(OwnerMobileEntity));
                var idResponse = await this.dataConnection.InsertOrReplaceAsync(idDocEntity, typeof(DocumentMobileEntity)).ConfigureAwait(false);
                var skippersResponse = await this.dataConnection.InsertOrReplaceAsync(skippersDocument, typeof(DocumentMobileEntity)).ConfigureAwait(false);
                var icasaResponse = await this.dataConnection.InsertOrReplaceAsync(icasaPopPhoto, typeof(DocumentMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Data Methods

        /// <inheritdoc/>
        public async Task<List<BoatMobileModel>> FindBoatsByOwnerIdAsync(Guid ownerId)
        {
            try
            {
                if (ownerId == null || ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var boats = await this.dataConnection.Table<BoatMobileEntity>().Where(x => x.OwnerId == ownerId).ToListAsync().ConfigureAwait(false);

                if (boats != null && boats.Count > 0)
                {
                    return boats.Select(x => BoatHelper.ToBoatModel(x)).ToList();
                }
                else
                {
                    return new List<BoatMobileModel>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateNewBoatAsync(BoatMobileModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }
                var boatEntity = BoatHelper.ToBoatEntity(boat);
                 var result = await this.dataConnection.InsertAsync(boatEntity, typeof(BoatMobileEntity)).ConfigureAwait(false);

                if (result > 0)
                {
                    return boatEntity.Id;
                }
                else
                {
                    throw new InvalidDataException("Could not save new boat details.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatMobileModel> FindBoatBySystemIdAsync(Guid systemId)
        {
            try
            {
                if (systemId == null || systemId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(systemId));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var boat = await this.dataConnection.Table<BoatMobileEntity>().FirstOrDefaultAsync(x => x.SystemId == systemId).ConfigureAwait(false);

                if (boat != null)
                {
                    return BoatHelper.ToBoatModel(boat);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatMobileModel> FindBoatByIdAsync(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var boat = await this.dataConnection.FindAsync<BoatMobileEntity>(id).ConfigureAwait(false);

                if (boat != null)
                {
                    return BoatHelper.ToBoatModel(boat);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateBoatAsync(BoatMobileModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var boatEntity = BoatHelper.ToBoatEntity(boat);
                var response = await this.dataConnection.UpdateAsync(boatEntity, typeof(BoatMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Data Methods

        /// <inheritdoc/>
        public async Task<Guid> CreateNewItemAsync(ItemMobileModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var itemEntity = ItemHelper.ToItemEntity(item);
                var response = await this.dataConnection.InsertAsync(itemEntity, typeof(ItemMobileEntity)).ConfigureAwait(false);

                return itemEntity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ItemMobileModel> FindItemBySystemIdAsync(Guid systemId)
        {
            try
            {
                if (systemId == null || systemId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(systemId));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var item = await this.dataConnection.Table<ItemMobileEntity>().FirstOrDefaultAsync(x => x.SystemId == systemId).ConfigureAwait(false);

                if (item != null)
                {
                    return ItemHelper.ToItemModel(item);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ItemMobileModel> FindItemByIdAsync(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var item = await this.dataConnection.FindAsync<ItemMobileEntity>(id).ConfigureAwait(false);

                if (item != null)
                {
                    return ItemHelper.ToItemModel(item);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<ItemMobileModel>> FindItemsByBoatIdAsync(Guid boatId)
        {
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var items = await this.dataConnection.Table<ItemMobileEntity>().Where(x => x.BoatId == boatId).ToListAsync().ConfigureAwait(false);

                return items.Select(z => ItemHelper.ToItemModel(z)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateItemAsync(ItemMobileModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var itemEntity = ItemHelper.ToItemEntity(item);
                var response = await this.dataConnection.UpdateAsync(itemEntity, typeof(ItemMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Image Methods

        public async Task<Guid> CreateNewImageAsync(DocumentMobileModel document)
        {
            try
            {
                if (document == null)
                {
                    throw new ArgumentNullException(nameof(document));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var imageEntity = ImageHelper.ToImageDataEntity(document);
                var response = await this.dataConnection.InsertAsync(imageEntity, typeof(DocumentMobileEntity));

                return imageEntity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateImageAsync(DocumentMobileModel document)
        {
            try
            {
                if (document == null)
                {
                    throw new ArgumentNullException(nameof(document));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var imageEntity = ImageHelper.ToImageDataEntity(document);
                var response = await this.dataConnection.UpdateAsync(imageEntity, typeof(DocumentMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DocumentMobileModel> FindImageByIdAsync(Guid documentId)
        {
            try
            {
                if (documentId == null || documentId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(documentId));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var response = await this.dataConnection.Table<DocumentMobileEntity>().FirstOrDefaultAsync(x => x.Id == documentId).ConfigureAwait(false);
                return ImageHelper.ToImageModel(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Creates all the necessary local tables.
        /// </summary>
        private void CreateTables()
        {
            try
            {
                this.dataConnection.CreateTableAsync<OwnerMobileEntity>();
                this.dataConnection.CreateTableAsync<BoatMobileEntity>();
                this.dataConnection.CreateTableAsync<ItemMobileEntity>();
                this.dataConnection.CreateTableAsync<DocumentMobileEntity>();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        /// <summary>
        /// Initializes the database connection for use.
        /// </summary>
        private SQLiteAsyncConnection InitializeDBConnection()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlueMileCertification.db3");
            return new SQLiteAsyncConnection(folderPath);

        }

        #endregion

        #region Instance Fields

        private SQLiteAsyncConnection dataConnection;

        #endregion

        #endregion
    }
}
