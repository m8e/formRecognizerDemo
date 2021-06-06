using Contoso;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace function
{
    public class SendAnswer_Activity
    {
        private readonly IHttpClientFactory _httClientFactory;

        public SendAnswer_Activity(IHttpClientFactory clientFactory)
        {
            _httClientFactory = clientFactory;
        }


        [FunctionName("SendAnswer")]
        public async Task SendAnswerToWebhook([ActivityTrigger] Response answer, ILogger log)
        {
            try
            {
                var httpClient = _httClientFactory.CreateClient();
                await httpClient.PostAsJsonAsync(answer.WebHookUri,answer.Forms);
            }
            catch (System.Exception ex)
            {
                log.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
