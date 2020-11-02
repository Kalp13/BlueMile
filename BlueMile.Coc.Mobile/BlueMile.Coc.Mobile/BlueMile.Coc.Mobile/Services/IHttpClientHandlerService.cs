using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlueMile.Coc.Mobile.Services
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
