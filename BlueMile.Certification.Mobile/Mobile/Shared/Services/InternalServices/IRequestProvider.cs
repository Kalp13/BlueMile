using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public interface IRequestProvider
    {
        Task<string> GetAsync(string uri, string token = "");
        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);
    }
}
