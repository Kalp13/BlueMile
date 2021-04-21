using System;
using System.Net.Http;

namespace BlueMile.Certification.Web.ApiClient
{
    public class CertificationApiClient : ICertificationApiClient
    {
        #region Constructor

        public CertificationApiClient(string baseAddress, string token)
        {
            this.baseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
        }

        #endregion

        #region ICertification Implementation



        #endregion

        #region Instance Methods

        private HttpClient CreateClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },

            };
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            HttpClient client = new HttpClient(httpClientHandler, false);
            client.BaseAddress = new Uri($"{this.baseAddress}");
            client.Timeout = TimeSpan.FromMinutes(30);
            return client;
        }

        #endregion

        private HttpClient client;

        private string baseAddress;
    }
}
