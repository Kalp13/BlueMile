using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public static class SettingsService
    {
        #region ISettingsService Implementation

        public static string OwnerServiceAddress
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, @"{0}/Certification/owner", ServiceAddress);
            }
        }

        public static string BoatServiceAddress
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, @"{0}/Certification/Boat", ServiceAddress);
            }
        }

        public static string ItemServiceAddress
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, @"{0}/Certification/item", ServiceAddress);
            }
        }

        public static string ImageServiceAddress
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, @"{0}/Certification/image", ServiceAddress);
            }
        }

        public static string UserServiceAddress
        {
            get
            {
                return $"{ServiceAddress}/Users";
            }
        }

        public static string ServiceAddress
        {
            get
            {
                return Preferences.Get(serviceAddressKey, String.Empty);
            }
            set
            {
                Preferences.Set(serviceAddressKey, value);
            }
        }

        public static string Username
        {
            get
            {
                return Preferences.Get(usernameKey, String.Empty);
            }
            set
            {
                Preferences.Set(usernameKey, value);
            }
        }

        public static string Password
        {
            get
            {
                return Preferences.Get(passwordKey, String.Empty);
            }
            set
            {
                Preferences.Set(passwordKey, value);
            }
        }

        public static string OwnerId
        {
            get
            {
                return Preferences.Get(ownerIdKey, String.Empty);
            }
            set
            {
                Preferences.Set(ownerIdKey, value);
            }
        }

        public static string UserToken
        {
            get
            {
                return Preferences.Get(userTokenKey, String.Empty);
            }
            set
            {
                Preferences.Set(userTokenKey, value);
            }
        }

        #endregion

        #region Class Fields

        private const string serviceAddressKey = "serviceaddress_key";

        private const string usernameKey = "username_key";

        private const string ownerIdKey = "ownerId_key";

        private const string passwordKey = "password_key";

        private const string userTokenKey = "userToken_key";

        #endregion
    }
}
