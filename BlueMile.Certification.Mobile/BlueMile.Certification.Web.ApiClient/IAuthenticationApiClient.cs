using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Web.ApiClient
{
    public interface IAuthenticationApiClient
    {
        Task<bool> Register(UserRegistrationModel user);

        Task<bool> Login(UserLoginModel user);

        Task LogOut();
    }
}
