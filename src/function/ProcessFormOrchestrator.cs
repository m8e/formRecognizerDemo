using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Contoso
{
    public class ProcessFormOrchestrator 
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProcessFormOrchestrator(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [FunctionName("ExtractFormValue_Orchestrator")]
        public async Task<string> RunOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context,ILogger log)
        {
            
            try
            {
                var payload = context.GetInput<Payload>();
                var validator = new PayloadValidator();
                validator.ValidateAndThrow(payload);

                string jsonResponse = await context.CallActivityAsync<string>("GetFormValue",payload);

                // Send answer to webhook
                // var httpClient = _httpFactory.CreateClient();
                //await httpClient.PostAsJsonAsync(payload.WebHookUrl,forms);

                return jsonResponse;
            }
            catch (System.Exception ex)
            {
                log.LogError(ex.Message,ex);
                throw;
            }
        }

    }
}