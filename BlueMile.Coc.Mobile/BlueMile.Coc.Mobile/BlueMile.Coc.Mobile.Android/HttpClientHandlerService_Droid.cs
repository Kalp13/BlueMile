using System.Net.Http;
using BlueMile.Coc.Mobile.Droid;
using BlueMile.Coc.Mobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientHandlerService))]
namespace BlueMile.Coc.Mobile.Droid
{
    public class HttpClientHandlerService : IHttpClientHandlerService
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