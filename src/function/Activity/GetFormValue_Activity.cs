using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.AI.FormRecognizer;
using FluentValidation;
using Azure.AI.FormRecognizer.Models;
using System.Net.Http;
using System.Linq;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace Contoso
{
    public class GetFormValue
    {
        private readonly FormRecognizerClient _formRecognizer;
        
        public GetFormValue(FormRecognizerClient client)
        {
            _formRecognizer = client;            
        }

        [FunctionName("GetFormValue")]
        public async Task<string> SayHello([ActivityTrigger] Payload payload, ILogger log)
        {
            try
            {
                RecognizedFormCollection forms = await _formRecognizer.StartRecognizeCustomFormsFromUri(payload.ModelId,
                                                                                                        new Uri(payload.FileUri))
                                                                      .WaitForCompletionAsync();

                return JsonConvert.SerializeObject(forms);
            }
            catch (System.Exception ex)
            {
                log.LogError(ex.Message,ex);
                throw;
            }
        }
    }
}
