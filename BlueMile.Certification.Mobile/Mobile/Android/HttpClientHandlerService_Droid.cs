using BlueMile.Certification.Mobile.Droid;
using BlueMile.Certification.Mobile.Services.InternalServices;
using System;
using System.Net.Http;
using Xamarin.Forms;


[assembly: Dependency(typeof(HttpClientHandlerService_Droid))]
namespace BlueMile.Certification.Mobile.Droid
{
    public class HttpClientHandlerService_Droid : IHttpClientHandlerService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                {
                    return true;
                }

                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}