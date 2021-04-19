using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Mobile.Services
{
    public static class Constants
    {
        public static string AuthorityUri = "https://www.bluemile.co.za";
        public static string AuthorizeUri = "https://www.bluemile.co.za/connect/authorize";
        public static string TokenUri = "https://www.bluemile.co.za/connect/token";
        public static string RedirectUri = "io.identitymodel.native://callback";
        public static string ApiUri = "https://www.bluemile.co.za/api/";
        public static string ClientId = "native.hybrid";
        public static string ClientSecret = "xoxo";
        public static string Scope = "openid profile api";
    }
}
