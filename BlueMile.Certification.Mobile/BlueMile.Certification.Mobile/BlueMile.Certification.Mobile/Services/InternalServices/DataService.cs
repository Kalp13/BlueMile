using BlueMile.Certification.Mobile.Data;
using BlueMile.Certification.Mobile.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        public async Task<bool> CreateNewOwnerAsync(OwnerMobileEntity owner)
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

                var response = await this.dataConnection.InsertOrReplaceAsync(owner, typeof(OwnerMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerMobileEntity> FindOwnerBySystemIdAsync(Guid systemId)
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

                return owner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerMobileEntity> FindOwnerByIdAsync(long id)
        {
            try
            {
                if (id < 1)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var owner = await this.dataConnection.FindAsync<OwnerMobileEntity>(id).ConfigureAwait(false);

                return owner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateOwnerAsync(OwnerMobileEntity owner)
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

                var response = await this.dataConnection.UpdateAsync(owner, typeof(OwnerMobileEntity));

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
        public async Task<bool> CreateNewBoatAsync(BoatMobileEntity boat)
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

                var response = await this.dataConnection.InsertOrReplaceAsync(boat, typeof(BoatMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatMobileEntity> FindBoatBySystemIdAsync(Guid systemId)
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

                return boat;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatMobileEntity> FindBoatByIdAsync(long id)
        {
            try
            {
                if (id < 1)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var owner = await this.dataConnection.FindAsync<BoatMobileEntity>(id).ConfigureAwait(false);

                return owner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateBoatAsync(BoatMobileEntity boat)
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

                var response = await this.dataConnection.UpdateAsync(boat, typeof(BoatMobileEntity));

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
        public async Task<bool> CreateNewItemAsync(ItemMobileEntity item)
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

                var response = await this.dataConnection.InsertOrReplaceAsync(item, typeof(ItemMobileEntity)).ConfigureAwait(false);

                return response > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ItemMobileEntity> FindItemBySystemIdAsync(Guid systemId)
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

                var owner = await this.dataConnection.Table<ItemMobileEntity>().FirstOrDefaultAsync(x => x.SystemId == systemId).ConfigureAwait(false);

                return owner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ItemMobileEntity> FindItemByIdAsync(long id)
        {
            try
            {
                if (id < 1)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (this.dataConnection == null)
                {
                    this.dataConnection = this.InitializeDBConnection();
                }

                var owner = await this.dataConnection.FindAsync<ItemMobileEntity>(id).ConfigureAwait(false);

                return owner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateItemAsync(ItemMobileEntity item)
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

                var response = await this.dataConnection.UpdateAsync(item, typeof(ItemMobileEntity));

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
                this.dataConnection.CreateTableAsync<BoatMobileEntity>();
                this.dataConnection.CreateTableAsync<ItemMobileEntity>();
                this.dataConnection.CreateTableAsync<ImageMobileEntity>();
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
