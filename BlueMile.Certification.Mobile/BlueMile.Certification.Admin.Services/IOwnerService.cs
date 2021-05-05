using BlueMile.Certification.Admin.Services.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Services
{
    public interface IOwnerService
    {
        Task<PagedResponseModel<OwnerModel>> FindOwnersAsync(FindOwnersRequestModel request);

        Task<OwnerModel> FindOwnerBySystemIdAsync(Guid id);
    }
}
