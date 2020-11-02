using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Coc.Mobile.Services
{
    public class SqlDataService : ISqlDataService, IDisposable
    {
        #region Class Properties

        /// <summary>
        /// Gets or sets the local database connection to interact with the local database.
        /// </summary>
        private SQLiteAsyncConnection DatabaseConnection;

        #endregion

        #region Constructor

        public SqlDataService()
        {
            this.InitializeDBConnection();
            this.CreateTables();
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
                this.DatabaseConnection.CreateTableAsync<OwnerEntity>();
                this.DatabaseConnection.CreateTableAsync<BoatEntity>();
                this.DatabaseConnection.CreateTableAsync<RequiredItemEntity>();
                this.DatabaseConnection.CreateTableAsync<ImageEntity>();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        /// <summary>
        /// Initializes the database connection for use.
        /// </summary>
        private void InitializeDBConnection()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlueMileCOC.db3");
            this.DatabaseConnection = new SQLiteAsyncConnection(folderPath);
        }

        #endregion

        #region ISqlDataService Implementation

        #region Owner Methods

        /// </inheritdoc>
        public async Task<OwnerModel> GetOwnerById(Guid ownerId)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                var foundOwner = await this.DatabaseConnection.Table<OwnerEntity>().FirstOrDefaultAsync(x => x.Id == ownerId).ConfigureAwait(false);
                var ownerMapped = new OwnerModel
                {
                    Address = foundOwner.Address,
                    Id = foundOwner.Id,
                    CellNumber = foundOwner.CellNumber,
                    Email = foundOwner.Email,
                    IdentificationNumber = foundOwner.IdentificationNumber,
                    Name = foundOwner.Name,
                    SkippersLicenseNumber = foundOwner.SkippersLicenseNumber,
                    Surname = foundOwner.Surname,
                    VhfOperatorsLicense = foundOwner.VhfOperatorsLicense,
                    IsSynced = foundOwner.IsSynced
                };

                var identificationImage = await this.GetImageById(foundOwner.IdentificationDocumentId).ConfigureAwait(false);
                ownerMapped.IdentificationDocument = new ImageModel()
                {
                    Id = identificationImage.Id,
                    FileName = identificationImage.ImageName,
                    FilePath = identificationImage.ImagePath,
                    UniqueImageName = identificationImage.UniqueImageName
                };

                var skippersImage = await this.GetImageById(foundOwner.SkippersLicenseImageId).ConfigureAwait(false);
                ownerMapped.SkippersLicenseImage = new ImageModel()
                {
                    Id = skippersImage.Id,
                    FileName = skippersImage.ImageName,
                    FilePath = skippersImage.ImagePath,
                    UniqueImageName = skippersImage.UniqueImageName
                };

                var icasaImage = await this.GetImageById(foundOwner.IcasaPopPhotoId).ConfigureAwait(false);
                ownerMapped.IcasaPopPhoto = new ImageModel()
                {
                    Id = icasaImage.Id,
                    FileName = icasaImage.ImageName,
                    FilePath = icasaImage.ImagePath,
                    UniqueImageName = icasaImage.UniqueImageName
                };

                return ownerMapped;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<List<OwnerModel>> GetAllOwners()
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                var owners = await this.DatabaseConnection.Table<OwnerEntity>().ToListAsync().ConfigureAwait(false);
                var ownersMapped = owners.Select(x => new OwnerModel()
                {

                    Address = x.Address,
                    Id = x.Id,
                    CellNumber = x.CellNumber,
                    Email = x.Email,
                    IdentificationNumber = x.IdentificationNumber,
                    Name = x.Name,
                    SkippersLicenseNumber = x.SkippersLicenseNumber,
                    Surname = x.Surname,
                    VhfOperatorsLicense = x.VhfOperatorsLicense
                }).ToList<OwnerModel>();

                foreach (var owner in owners)
                {
                    var identificationImage = await this.GetImageById(owner.IdentificationDocumentId).ConfigureAwait(false);
                    ownersMapped.FirstOrDefault(x => x.Id == owner.Id).IdentificationDocument = new ImageModel()
                    {
                        Id = identificationImage.Id,
                        FileName = identificationImage.ImageName,
                        FilePath = identificationImage.ImagePath,
                        UniqueImageName = identificationImage.UniqueImageName
                    };

                    var skippersImage = await this.GetImageById(owner.SkippersLicenseImageId).ConfigureAwait(false);
                    ownersMapped.FirstOrDefault(x => x.Id == owner.Id).SkippersLicenseImage = new ImageModel()
                    {
                        Id = skippersImage.Id,
                        FileName = skippersImage.ImageName,
                        FilePath = skippersImage.ImagePath,
                        UniqueImageName = skippersImage.UniqueImageName
                    };

                    var icasaImage = await this.GetImageById(owner.IcasaPopPhotoId).ConfigureAwait(false);
                    ownersMapped.FirstOrDefault(x => x.Id == owner.Id).IcasaPopPhoto = new ImageModel()
                    {
                        Id = icasaImage.Id,
                        FileName = icasaImage.ImageName,
                        FilePath = icasaImage.ImagePath,
                        UniqueImageName = icasaImage.UniqueImageName
                    };
                }

                return ownersMapped;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<bool> CreateNewOwner(OwnerModel newOwner)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (newOwner == null)
                {
                    throw new ArgumentNullException(nameof(newOwner));
                }

                var response = await this.DatabaseConnection.InsertOrReplaceAsync(new OwnerEntity
                {
                    Address = newOwner.Address,
                    CellNumber = newOwner.CellNumber,
                    Email = newOwner.Email,
                    IdentificationNumber = newOwner.IdentificationNumber,
                    Name = newOwner.Name,
                    SkippersLicenseNumber = newOwner.SkippersLicenseNumber,
                    Surname = newOwner.Surname,
                    Id = newOwner.Id,
                    VhfOperatorsLicense = newOwner.VhfOperatorsLicense,
                    IcasaPopPhotoId = newOwner.IcasaPopPhoto.Id,
                    IdentificationDocumentId = newOwner.IdentificationDocument.Id,
                    SkippersLicenseImageId = newOwner.SkippersLicenseImage.Id,
                    IsSynced = false
                }, typeof(OwnerEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<bool> UpdateOwner(OwnerModel updatedOwner)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (updatedOwner == null)
                {
                    throw new ArgumentNullException(nameof(updatedOwner));
                }

                var response = await this.DatabaseConnection.UpdateAsync(new OwnerEntity
                {
                    Id = updatedOwner.Id,
                    Address = updatedOwner.Address,
                    CellNumber = updatedOwner.CellNumber,
                    Email = updatedOwner.Email,
                    IdentificationNumber = updatedOwner.IdentificationNumber,
                    IdentificationDocumentId = updatedOwner.IdentificationDocument.Id,
                    Name = updatedOwner.Name,
                    SkippersLicenseNumber = updatedOwner.SkippersLicenseNumber,
                    Surname = updatedOwner.Surname,
                    VhfOperatorsLicense = updatedOwner.VhfOperatorsLicense,
                    IcasaPopPhotoId = updatedOwner.IcasaPopPhoto.Id,
                    SkippersLicenseImageId = updatedOwner.SkippersLicenseImage.Id,
                    IsSynced = updatedOwner.IsSynced
                }, typeof(OwnerEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Methods

        /// </inheritdoc>
        public async Task<bool> CreateNewBoat(BoatModel newBoat)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (newBoat == null)
                {
                    throw new ArgumentNullException(nameof(newBoat));
                }

                var response = await this.DatabaseConnection.InsertOrReplaceAsync(new BoatEntity
                {
                    BoyancyCertificateImageId = newBoat.BoyancyCertificateImage.Id,
                    BoyancyCertificateNumber = newBoat.BoyancyCertificateNumber,
                    CategoryId = newBoat.CategoryId,
                    Name = newBoat.Name,
                    Id = newBoat.Id,
                    OwnerId = newBoat.OwnerId,
                    RegisteredNumber = newBoat.RegisteredNumber,
                    IsJetski = newBoat.IsJetski,
                    TubbiesCertificateImageId = newBoat.TubbiesCertificateImage.Id,
                    TubbiesCertificateNumber = newBoat.TubbiesCertificateNumber
                }).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<List<BoatModel>> GetAllBoats(Guid ownerId)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (ownerId == null || (ownerId == Guid.Empty))
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                var boats = await this.DatabaseConnection.Table<BoatEntity>().Where(x => x.OwnerId == ownerId).ToListAsync().ConfigureAwait(false);
                List<BoatModel> boatsMapped = boats.Select(x => new BoatModel
                {
                    BoyancyCertificateNumber = x.BoyancyCertificateNumber,
                    CategoryId = x.CategoryId,
                    Id = x.Id,
                    Name = x.Name,
                    OwnerId = x.OwnerId,
                    RegisteredNumber = x.RegisteredNumber,
                    IsJetski = x.IsJetski,
                    TubbiesCertificateNumber = x.TubbiesCertificateNumber
                }).ToList();

                foreach (var boat in boats)
                {
                    var boyancyImage = (await this.GetImageById(boat.BoyancyCertificateImageId).ConfigureAwait(false));
                    boatsMapped.FirstOrDefault(x => x.Id == boat.Id).BoyancyCertificateImage = new ImageModel()
                    {
                        Id = boyancyImage.Id,
                        FileName = boyancyImage.ImageName,
                        FilePath = boyancyImage.ImagePath,
                        UniqueImageName = boyancyImage.UniqueImageName
                    };

                    if (boat.IsJetski)
                    {
                        var tubbiesImage = (await this.GetImageById(boat.TubbiesCertificateImageId).ConfigureAwait(false));
                        boatsMapped.FirstOrDefault(x => x.Id == boat.Id).TubbiesCertificateImage = new ImageModel()
                        {
                            Id = tubbiesImage.Id,
                            FileName = tubbiesImage.ImageName,
                            FilePath = tubbiesImage.ImagePath,
                            UniqueImageName = tubbiesImage.UniqueImageName
                        };
                    }
                }

                return boatsMapped;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<BoatModel> GetBoatById(Guid boatId)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (boatId == null || (boatId == Guid.Empty))
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                var foundBoat = await this.DatabaseConnection.Table<BoatEntity>().FirstOrDefaultAsync(x => x.Id == boatId).ConfigureAwait(false);
                var boatMapped = new BoatModel
                {
                    BoyancyCertificateNumber = foundBoat.BoyancyCertificateNumber,
                    CategoryId = foundBoat.CategoryId,
                    Id = foundBoat.Id,
                    Name = foundBoat.Name,
                    OwnerId = foundBoat.OwnerId,
                    RegisteredNumber = foundBoat.RegisteredNumber,
                    IsJetski = foundBoat.IsJetski,
                    TubbiesCertificateNumber = foundBoat.TubbiesCertificateNumber
                };

                var boyancyImage = await this.GetImageById(foundBoat.BoyancyCertificateImageId).ConfigureAwait(false);
                boatMapped.BoyancyCertificateImage = new ImageModel()
                {
                    Id = boyancyImage.Id,
                    FileName = boyancyImage.ImageName,
                    FilePath = boyancyImage.ImagePath,
                    UniqueImageName = boyancyImage.UniqueImageName
                };

                if (boatMapped.IsJetski)
                {
                    var tubbiesImage = await this.GetImageById(foundBoat.TubbiesCertificateImageId).ConfigureAwait(false);
                    boatMapped.TubbiesCertificateImage = new ImageModel()
                    {
                        Id = tubbiesImage.Id,
                        FileName = tubbiesImage.ImageName,
                        FilePath = tubbiesImage.ImagePath,
                        UniqueImageName = tubbiesImage.UniqueImageName
                    };
                }

                return boatMapped;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<bool> UpdateBoat(BoatModel updatedBoat)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (updatedBoat == null)
                {
                    throw new ArgumentNullException(nameof(updatedBoat));
                }

                var convertedBoat = new BoatEntity
                {
                    Id = updatedBoat.Id,
                    BoyancyCertificateImageId = updatedBoat.BoyancyCertificateImage.Id,
                    BoyancyCertificateNumber = updatedBoat.BoyancyCertificateNumber,
                    CategoryId = updatedBoat.CategoryId,
                    Name = updatedBoat.Name,
                    OwnerId = updatedBoat.OwnerId,
                    RegisteredNumber = updatedBoat.RegisteredNumber,
                    IsJetski = updatedBoat.IsJetski,
                    TubbiesCertificateImageId = updatedBoat.TubbiesCertificateImage.Id,
                    TubbiesCertificateNumber = updatedBoat.TubbiesCertificateNumber
                };
                var response = await this.DatabaseConnection.UpdateAsync(convertedBoat, typeof(BoatEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Methods

        /// </inheritdoc>
        public async Task<bool> CreateNewItem(RequiredItemModel requiredItem)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (requiredItem == null)
                {
                    throw new ArgumentNullException(nameof(requiredItem));
                }

                var itemEntity = new RequiredItemEntity
                {
                    BoatId = requiredItem.BoatId,
                    CapturedDate = requiredItem.CapturedDate,
                    Description = requiredItem.Description,
                    ExpiryDate = requiredItem.ExpiryDate,
                    ItemImageId = requiredItem.ItemImage.Id,
                    Id = requiredItem.Id,
                    ItemTypeId = requiredItem.ItemTypeId,
                    SerialNumber = requiredItem.SerialNumber
                };

                var response = await this.DatabaseConnection.InsertOrReplaceAsync(itemEntity).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<bool> UpdateRequireditem(RequiredItemModel requiredItem)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (requiredItem == null)
                {
                    throw new ArgumentNullException(nameof(requiredItem));
                }

                var response = await this.DatabaseConnection.UpdateAsync(new RequiredItemEntity
                {
                    Id = requiredItem.Id,
                    BoatId = requiredItem.BoatId,
                    CapturedDate = requiredItem.CapturedDate,
                    Description = requiredItem.Description,
                    ExpiryDate = requiredItem.ExpiryDate,
                    ItemImageId = requiredItem.ItemImage.Id,
                    ItemTypeId = requiredItem.ItemTypeId,
                    SerialNumber = requiredItem.SerialNumber
                }).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// </inheritdoc>
        public async Task<List<RequiredItemModel>> GetItemsByBoatId(Guid boatId)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                var items = await this.DatabaseConnection.Table<RequiredItemEntity>().Where(x => x.BoatId == boatId).ToListAsync().ConfigureAwait(false);
                var itemsMapped = items.Select(x => new RequiredItemModel
                {
                    BoatId = x.BoatId,
                    CapturedDate = x.CapturedDate,
                    Description = x.Description,
                    ExpiryDate = x.ExpiryDate,
                    Id = x.Id,
                    ItemTypeId = x.ItemTypeId,
                    SerialNumber = x.SerialNumber
                }).ToList();

                return itemsMapped;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RequiredItemModel> GetItemById(Guid itemId)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (itemId == null || itemId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(itemId));
                }

                var item = await this.DatabaseConnection.Table<RequiredItemEntity>().FirstOrDefaultAsync(x => x.Id == itemId).ConfigureAwait(false);
                var mappedItem = new RequiredItemModel
                {
                    BoatId = item.BoatId,
                    CapturedDate = item.CapturedDate,
                    Description = item.Description,
                    ExpiryDate = item.ExpiryDate,
                    Id = item.Id,
                    ItemTypeId = item.ItemTypeId,
                    SerialNumber = item.SerialNumber
                };

                var itemImage = await this.GetImageById(item.ItemImageId).ConfigureAwait(false);
                mappedItem.ItemImage = new ImageModel()
                {
                    FileName = itemImage.ImageName,
                    FilePath = itemImage.ImagePath,
                    Id = itemImage.Id,
                    UniqueImageName = itemImage.UniqueImageName
                };

                return mappedItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Image Methods

        public async Task<bool> CreateNewImage(ImageModel image)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }

                var response = await this.DatabaseConnection.InsertOrReplaceAsync(new ImageEntity()
                {
                    Id = image.Id,
                    ImageData = File.ReadAllBytes(image.FilePath),
                    ImageName = image.FileName,
                    ImagePath = image.FilePath,
                    UniqueImageName = image.UniqueImageName
                }).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateImage(ImageModel image)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }

                var response = await this.DatabaseConnection.InsertOrReplaceAsync(new ImageEntity()
                {
                    Id = image.Id,
                    ImageData = File.ReadAllBytes(image.FilePath),
                    ImagePath = image.FilePath,
                    ImageName = image.FileName,
                    UniqueImageName = image.UniqueImageName
                }).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ImageEntity> GetImageById(Guid imageId)
        {
            try
            {
                if (this.DatabaseConnection == null)
                {
                    throw new ArgumentNullException(paramName: nameof(this.DatabaseConnection));
                }

                if (imageId == null)
                {
                    throw new ArgumentNullException(nameof(imageId));
                }

                var response = await this.DatabaseConnection.Table<ImageEntity>().FirstOrDefaultAsync(Xamarin => Xamarin.Id == imageId).ConfigureAwait(false);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            this.DatabaseConnection.CloseAsync();
        }

        #endregion
    }
}
