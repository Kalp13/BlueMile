using BlueMile.Certification.Admin.Services.Models;
using BlueMile.Certification.Data;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Services
{
    public class OwnerService : IOwnerService, IDisposable
    {
        #region Constructor

        public OwnerService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        #endregion

        public Task<PagedResponseModel<OwnerModel>> FindOwnersAsync(FindOwnersRequestModel request)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<OwnerModel> FindOwnerBySystemIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #region Instance Fields

        /// <summary>
        /// Provides access to the underlying data store.
        /// </summary>
        private IDbContextFactory<ApplicationDbContext> dbFactory;

        #endregion
    }
}
