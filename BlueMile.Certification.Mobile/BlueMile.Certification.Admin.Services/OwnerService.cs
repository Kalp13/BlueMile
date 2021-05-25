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

                var query = dbContext.Owners.AsQueryable();

                if (request.OwnerId.HasValue)
                {
                    query = query.Where(x => x.Id == request.OwnerId);
                }

                if (!String.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    query = query.Where(x => x.Identification.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                             x.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                             x.Surname.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                             x.Email.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                             x.ContactNumber.ToLower().Contains(request.SearchTerm.ToLower()));
                }

                var total = await query.Where(x => x.IsActive).CountAsync();

                var owners = await query.Where(x => x.IsActive).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToArrayAsync();
                var results = owners.Select(c => new OwnerWebModel()
                {
                    AddressLine1 = c.AddressLine1,
                    AddressLine2 = c.AddressLine2,
                    AddressLine3 = c.AddressLine3,
                    AddressLine4 = c.AddressLine4,
                    ContactNumber = c.ContactNumber,
                    Country = c.Country,
                    Email = c.Email,
                    Identification = c.Identification,
                    Name = c.Name,
                    PostalCode = c.PostalCode,
                    Province = c.Province,
                    SkippersLicenseNumber = c.SkippersLicenseNumber,
                    Suburb = c.Suburb,
                    Surname = c.Surname,
                    SystemId = c.Id,
                    Town = c.Town,
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
