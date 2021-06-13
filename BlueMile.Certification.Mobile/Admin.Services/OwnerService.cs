using BlueMile.Certification.Admin.Services.Models;
using BlueMile.Certification.Data;
using BlueMile.Certification.Web.ApiClient;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Services
{
    public class OwnerService : IOwnerService, IDisposable
    {
        #region Constructor

        public string Token { get; set; }

        public OwnerService(IDbContextFactory<ApplicationDbContext> dbFactory,
                            string baseAddress)
        {
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            this.baseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
        }

        #endregion

        public async Task<PagedResponseModel<OwnerWebModel>> FindOwnersAsync(FindOwnerModel request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var dbContext = this.dbFactory.CreateDbContext();

                var query = dbContext.IndividualsOwners.AsQueryable();

                if (request.OwnerId.HasValue)
                {
                    query = query.Where(x => x.Id == request.OwnerId);
                }

                if (!String.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    query = query.Where(x => x.Identification.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                             x.FirstName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                             x.LastName.ToLower().Contains(request.SearchTerm.ToLower()));
                }

                var total = await query.Where(x => x.IsActive).CountAsync();

                var owners = await query.Where(x => x.IsActive).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToArrayAsync();
                var results = owners.Select(c => new OwnerWebModel()
                {
                    Identification = c.Identification,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SkippersLicenseNumber = c.SkippersLicenseNumber,
                    SystemId = c.Id,
                    VhfOperatorsLicense = c.VhfOperatorsLicense
                }).ToArray();

                return new PagedResponseModel<OwnerWebModel>()
                {
                    Items = results,
                    Page = request.Page,
                    PageSize = request.PageSize,
                    Total = total
                };
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        private ICertificationApiClient CreateApiClient()
        {
            return new CertificationApiClient(this.baseAddress, this.Token);
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

        private readonly IHttpClientFactory clientFactory;

        private string baseAddress;

        #endregion
    }
}
