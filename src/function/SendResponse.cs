using Contoso;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace function
{
    public class SendResponse
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SendResponse(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [FunctionName("ProcessResponse")]
        public async Task Run([ServiceBusTrigger("processedForm", Connection = "StrBusCnx")] string mySbMsg,
                              ILogger log)
        {

            var document = JsonConvert.DeserializeObject<Document>(mySbMsg);

            var http = _httpClientFactory.CreateClient();

            await http.PostAsync(document.WebHook, new StringContent(JsonConvert.SerializeObject(
                                                                     new 
                                                                     { 
                                                                         DocumentId = document.DocumentId, 
                                                                         ExtractedText = document.ExtractedText 
                                                                     }), Encoding.UTF8, "application/json"));

        }
    }
}
