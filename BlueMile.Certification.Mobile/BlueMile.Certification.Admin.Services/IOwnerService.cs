using BlueMile.Certification.Admin.Services.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Services
{
    public interface IOwnerService
    {
        Task<PagedResponseModel<OwnerWebModel>> FindOwnersAsync(FindOwnerModel request);

        Task<OwnerModel> FindOwnerBySystemIdAsync(Guid id);
    }
}
