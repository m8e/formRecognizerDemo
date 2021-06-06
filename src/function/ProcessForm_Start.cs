using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Contoso
{
    public static class ProcessForm
    {
        [FunctionName("ExtractFormValue_Start")]
        public static async Task<HttpResponseMessage> HttpStart([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestMessage req,
                                                                [DurableClient] IDurableOrchestrationClient starter,
                                                                ILogger log)
        {

            var body = await req.Content.ReadAsStringAsync();
            var payload = JsonConvert.DeserializeObject<Payload>(body);
            var modelId = Environment.GetEnvironmentVariable("ModelId");
            payload.ModelId = modelId;
            
            string instanceId = await starter.StartNewAsync("ExtractFormValue_Orchestrator", null, payload);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}