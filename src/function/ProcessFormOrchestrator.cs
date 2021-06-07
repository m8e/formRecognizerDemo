using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Contoso
{
    //public class ProcessFormOrchestrator 
    //{
    //    [FunctionName("ExtractFormValue_Orchestrator")]
    //    public async Task<string> RunOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context,ILogger log)
    //    {
            
    //        try
    //        {
    //            var payload = context.GetInput<Payload>();
    //            var validator = new PayloadValidator();
    //            validator.ValidateAndThrow(payload);

    //            string jsonResponse = await context.CallActivityAsync<string>("GetFormValue",payload);
    //            var response = new Response 
    //            { 
    //                WebHookUri = payload.WebHookUrl,
    //                Forms = jsonResponse
    //            };

    //            await context.CallActivityAsync<string>("SendAnswer", response);

    //            return jsonResponse;
    //        }
    //        catch (Exception ex)
    //        {
    //            log.LogError(ex.Message,ex);
    //            throw;
    //        }
    //    }

    //}
}