using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:44390";

        public static string CertificationEndpoint = $"{BaseUrl}/api/certification";

        public static string RegisterEndpoint = $"{BaseUrl}/api/users/register";

        public static string LoginEndpoint = $"{BaseUrl}/api/users/login";
    }
}
