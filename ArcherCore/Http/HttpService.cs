using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Http
{
    public class HttpService
    {
        public IHttpClientFactory httpFactory;
        public HttpService(IHttpClientFactory httpFactory)
        {
            this.httpFactory = httpFactory;
        }
        public IHttpClientFactory HttpClientFactory()
        {
            return httpFactory;
        }
    }
}
