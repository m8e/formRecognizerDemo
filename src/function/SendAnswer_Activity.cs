using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace function
{
    public class SendAnswer_Activity
    {
        private readonly IHttpClientFactory _httClientFactory;

        public SendAnswer_Activity(IHttpClientFactory clientFactory)
        {
            _httClientFactory = clientFactory;
        }
    }
}
