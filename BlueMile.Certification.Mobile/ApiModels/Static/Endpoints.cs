using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = @"https://localhost:5001";

        public static string CertificationEndpoint = $@"{BaseUrl}/api/Certification";

        public static string RegisterEndpoint = $@"{BaseUrl}/api/Users/register";

        public static string LoginEndpoint = $@"{BaseUrl}/api/Users/login";
    }
}
