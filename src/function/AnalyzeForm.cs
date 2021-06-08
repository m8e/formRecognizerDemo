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
using Azure.AI.FormRecognizer.Models;
using FluentValidation;

namespace Contoso
{
    public class AnalyzeForm
    {
        private readonly FormRecognizerClient _formRecognizer;

        public AnalyzeForm(FormRecognizerClient client)
        {
            _formRecognizer = client;
        }

        [FunctionName("AnalyzeForm")]
        [return: ServiceBus("processedForm", Connection = "StrBusCnx")]
        public async Task<string> Run([ServiceBusTrigger("processForm", Connection = "StrBusCnx")] string mySbMsg,
                                      ILogger log)
        {

            var payload = JsonConvert.DeserializeObject<Payload>(mySbMsg);
            payload.ModelId = Environment.GetEnvironmentVariable("ModelId");

            var validator = new PayloadValidator();
            validator.ValidateAndThrow(payload);
            
            // To not overload the TPS of Form Recognizer we added a 10 seconds delay for the pulling result
            RecognizedFormCollection forms = await _formRecognizer.StartRecognizeCustomFormsFromUri(payload.ModelId,
                                                                                                    new Uri(payload.FileUri))
                                                                  .WaitForCompletionAsync(new TimeSpan(0, 0, 10));

            var serializedObject = JsonConvert.SerializeObject(forms);

            return serializedObject;

        }      
    }
}
