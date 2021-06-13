using BlueMile.Certification.Mobile.Data.Helpers;
using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Data.Static;
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

                var response = await this.dataConnection.InsertAsync(ownerEntity, typeof(OwnerMobileEntity)).ConfigureAwait(false);

                if (owner.IdentificationDocument != null && !String.IsNullOrWhiteSpace(owner.IdentificationDocument.FilePath))
                {
                    owner.IdentificationDocument.OwnerId = ownerEntity.Id;
                    var idDocEntity = OwnerHelper.ToOwnerDocumentEntity(owner.IdentificationDocument);
                    await this.dataConnection.InsertOrReplaceAsync(idDocEntity, typeof(ItemDocumentMobileEntity)).ConfigureAwait(false);
                }
                if (owner.SkippersLicenseImage != null && !String.IsNullOrWhiteSpace(owner.SkippersLicenseImage.FilePath))
                {
                    owner.SkippersLicenseImage.OwnerId = ownerEntity.Id;
                    var skippersDocument = OwnerHelper.ToOwnerDocumentEntity(owner.SkippersLicenseImage);
                    await this.dataConnection.InsertOrReplaceAsync(skippersDocument, typeof(ItemDocumentMobileEntity)).ConfigureAwait(false);
                }
                if (owner.IcasaPopPhoto != null && !String.IsNullOrWhiteSpace(owner.IcasaPopPhoto.FilePath))
                {
                    owner.IcasaPopPhoto.OwnerId = ownerEntity.Id;
                    var icasaPopPhoto = OwnerHelper.ToOwnerDocumentEntity(owner.IcasaPopPhoto);
                    await this.dataConnection.InsertOrReplaceAsync(icasaPopPhoto, typeof(ItemDocumentMobileEntity)).ConfigureAwait(false);
                }

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

                var owner = await this.dataConnection.Table<OwnerMobileEntity>().FirstOrDefaultAsync(x => x.Id == systemId).ConfigureAwait(false);

                if (owner != null)
                {
                    var result = OwnerHelper.ToOwnerModel(owner);
                    var idDoc = await this.dataConnection.Table<OwnerDocumentMobileEntity>().FirstOrDefaultAsync(x => x.OwnerId == result.Id && x.DocumentTypeId == (int)DocumentTypeEnum.IdentificationDocument);
                    if (idDoc != null)
                    {
                        result.IdentificationDocument = OwnerHelper.ToOwnerDocumentModel(idDoc);
                    }

                    var skipDoc = await this.dataConnection.Table<OwnerDocumentMobileEntity>().FirstOrDefaultAsync(x => x.OwnerId == result.Id && x.DocumentTypeId == (int)DocumentTypeEnum.SkippersLicense);
                    if (skipDoc != null)
                    {
                        result.SkippersLicenseImage = OwnerHelper.ToOwnerDocumentModel(skipDoc);
                    }

                    var icasaDoc = await this.dataConnection.Table<OwnerDocumentMobileEntity>().FirstOrDefaultAsync(x => x.OwnerId == result.Id && x.DocumentTypeId == (int)DocumentTypeEnum.IcasaProofOfPayment);
                    if (icasaDoc != null)
                    {
                        result.IcasaPopPhoto = OwnerHelper.ToOwnerDocumentModel(icasaDoc);
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
                    var documents = await this.dataConnection.Table<OwnerDocumentMobileEntity>().Where(x => x.OwnerId == owner.Id).ToListAsync();
                    var mappedOwner = OwnerHelper.ToOwnerModel(owner);

                    if (documents != null && documents.Count > 0)
                    {
                        var idDoc = documents.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.IdentificationDocument);
                        if (idDoc != null)
                        {
                            mappedOwner.IdentificationDocument = OwnerHelper.ToOwnerDocumentModel(idDoc);
                        }

                        var skippersDoc = documents.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.SkippersLicense);
                        if (skippersDoc != null)
                        {
                            mappedOwner.SkippersLicenseImage = OwnerHelper.ToOwnerDocumentModel(skippersDoc);
                        }

                        var icasaDoc = documents.FirstOrDefault(x => x.DocumentTypeId == (int)DocumentTypeEnum.IcasaProofOfPayment);
                        if (icasaDoc != null)
                        {
                            mappedOwner.IcasaPopPhoto = OwnerHelper.ToOwnerDocumentModel(icasaDoc);
                        }
                    }

                    return mappedOwner;
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

                var response = await this.dataConnection.UpdateAsync(ownerEntity, typeof(OwnerMobileEntity));

                if (owner.IdentificationDocument != null && !String.IsNullOrWhiteSpace(owner.IdentificationDocument.FilePath))
                {
                    owner.IdentificationDocument.OwnerId = owner.IdentificationDocument.OwnerId != Guid.Empty ?
                                                           owner.IdentificationDocument.OwnerId :
                                                           ownerEntity.Id;
                    var idDocEntity = OwnerHelper.ToOwnerDocumentEntity(owner.IdentificationDocument);
                    await this.dataConnection.InsertOrReplaceAsync(idDocEntity, typeof(OwnerDocumentMobileEntity)).ConfigureAwait(false);
                }
                if (owner.SkippersLicenseImage != null && !String.IsNullOrWhiteSpace(owner.SkippersLicenseImage.FilePath))
                {
                    owner.SkippersLicenseImage.OwnerId = owner.SkippersLicenseImage.OwnerId != Guid.Empty ?
                                                         owner.SkippersLicenseImage.OwnerId :
                                                         ownerEntity.Id;
                    var skippersDocument = OwnerHelper.ToOwnerDocumentEntity(owner.SkippersLicenseImage);
                    await this.dataConnection.InsertOrReplaceAsync(skippersDocument, typeof(OwnerDocumentMobileEntity)).ConfigureAwait(false);
                }
                if (owner.IcasaPopPhoto != null && !String.IsNullOrWhiteSpace(owner.IcasaPopPhoto.FilePath))
                {
                    owner.IcasaPopPhoto.OwnerId = owner.IcasaPopPhoto.OwnerId != Guid.Empty ?
                                                  owner.IcasaPopPhoto.OwnerId :
                                                  ownerEntity.Id;
                    var icasaPopPhoto = OwnerHelper.ToOwnerDocumentEntity(owner.IcasaPopPhoto);
                    await this.dataConnection.InsertOrReplaceAsync(icasaPopPhoto, typeof(OwnerDocumentMobileEntity)).ConfigureAwait(false);
                }

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
                if (boat.BoyancyCertificateImage != null && !String.IsNullOrWhiteSpace(boat.BoyancyCertificateImage.FilePath))
                {
                    boat.BoyancyCertificateImage.BoatId = boatEntity.Id;
                    var boyancyDocEntity = BoatHelper.ToBoatDocumentEntity(boat.BoyancyCertificateImage);
                    await this.dataConnection.InsertAsync(boyancyDocEntity, typeof(BoatDocumentMobileEntity)).ConfigureAwait(false);
                }
                if (boat.TubbiesCertificateImage != null && !String.IsNullOrWhiteSpace(boat.TubbiesCertificateImage.FilePath))
                {
                    boat.TubbiesCertificateImage.BoatId = boatEntity.Id;
                    var tubbiesDocEntity = BoatHelper.ToBoatDocumentEntity(boat.TubbiesCertificateImage);
                    await this.dataConnection.InsertAsync(tubbiesDocEntity, typeof(BoatDocumentMobileEntity)).ConfigureAwait(false);
                }
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

                var boat = await this.dataConnection.Table<BoatMobileEntity>().FirstOrDefaultAsync(x => x.Id == systemId).ConfigureAwait(false);

                if (boat != null)
                {
                    var mappedBoat = BoatHelper.ToBoatModel(boat);

                    var boyancyDoc = await this.dataConnection.Table<BoatDocumentMobileEntity>().FirstOrDefaultAsync(x => x.BoatId == mappedBoat.Id && x.DocumentTypeId == (int)DocumentTypeEnum.BoatBoyancyCertificate);
                    if (boyancyDoc != null)
                    {
                        mappedBoat.BoyancyCertificateImage = BoatHelper.ToBoatDocumentModel(boyancyDoc);
                    }

                    var tubbiesDoc = await this.dataConnection.Table<BoatDocumentMobileEntity>().FirstOrDefaultAsync(x => x.BoatId == mappedBoat.Id && x.DocumentTypeId == (int)DocumentTypeEnum.BoatBoyancyCertificate);
                    if (tubbiesDoc != null)
                    {
                        mappedBoat.TubbiesCertificateImage = BoatHelper.ToBoatDocumentModel(tubbiesDoc);
                    }
                    return mappedBoat;
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
                if (boat.BoyancyCertificateImage != null && !String.IsNullOrWhiteSpace(boat.BoyancyCertificateImage.FilePath))
                {
                    var boyancyDocEntity = BoatHelper.ToBoatDocumentEntity(boat.BoyancyCertificateImage);
                    await this.dataConnection.InsertOrReplaceAsync(boyancyDocEntity, typeof(BoatDocumentMobileEntity)).ConfigureAwait(false);
                }
                if (boat.TubbiesCertificateImage != null && !String.IsNullOrWhiteSpace(boat.TubbiesCertificateImage.FilePath))
                {
                    var tubbiesDocEntity = BoatHelper.ToBoatDocumentEntity(boat.TubbiesCertificateImage);
                    await this.dataConnection.InsertOrReplaceAsync(tubbiesDocEntity, typeof(BoatDocumentMobileEntity)).ConfigureAwait(false);
                }
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
                if (item.ItemImage != null && !String.IsNullOrWhiteSpace(item.ItemImage.FilePath))
                {
                    var itemDocEntity = ItemHelper.ToItemDocumentEntity(item.ItemImage);
                    await this.dataConnection.InsertOrReplaceAsync(itemDocEntity, typeof(ItemDocumentMobileEntity)).ConfigureAwait(false);
                }
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

                var item = await this.dataConnection.Table<ItemMobileEntity>().FirstOrDefaultAsync(x => x.Id == systemId).ConfigureAwait(false);

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
                if (item.ItemImage != null && !String.IsNullOrWhiteSpace(item.ItemImage.FilePath))
                {
                    var itemDocEntity = ItemHelper.ToItemDocumentEntity(item.ItemImage);
                    await this.dataConnection.InsertOrReplaceAsync(itemDocEntity, typeof(ItemDocumentMobileEntity)).ConfigureAwait(false);
                }
                var response = await this.dataConnection.UpdateAsync(itemEntity, typeof(ItemMobileEntity)).ConfigureAwait(false);

                return response > 0;
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
                this.dataConnection.CreateTableAsync<OwnerDocumentMobileEntity>();

                this.dataConnection.CreateTableAsync<BoatMobileEntity>();
                this.dataConnection.CreateTableAsync<BoatDocumentMobileEntity>();

                this.dataConnection.CreateTableAsync<ItemMobileEntity>();
                this.dataConnection.CreateTableAsync<ItemDocumentMobileEntity>();
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
