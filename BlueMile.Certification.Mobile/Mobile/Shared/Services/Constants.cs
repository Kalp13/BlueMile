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

        #region Routes

        // Owner Routes
        public static string ownersRoute = "owner/find";
        public static string ownerDetailRoute = "owner/detail";
        public static string ownerEditRoute = "owner/edit";

        // Boat Routes
        public static string boatsRoute = "/boat/find";
        public static string boatDetailRoute = "/boat/detail";
        public static string boatEditRoute = "/boat/edit";

        // Item Routes
        public static string itemsRoute = "/item/find";
        public static string itemDetailRoute = "./item/detail";
        public static string itemEditRoute = "./item/edit";
        public static string itemNewRoute = "./item/new";

        //Extra Routes
        public static string settingsRoute = "settings";

        #endregion
    }
}
