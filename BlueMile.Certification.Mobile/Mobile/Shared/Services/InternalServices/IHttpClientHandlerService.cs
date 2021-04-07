using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
