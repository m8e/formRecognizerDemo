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
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var payload = JsonConvert.DeserializeObject<Payload>(requestBody);
            payload.ModelId = Environment.GetEnvironmentVariable("ModelId");

            //var validator = new PayloadValidator();
            //validator.ValidateAndThrow(payload);

            RecognizedFormCollection forms = await _formRecognizer.StartRecognizeCustomFormsFromUri(payload.ModelId,
                                                                                                    new Uri(payload.FileUri))
                                                                  .WaitForCompletionAsync();

            var serializedObject = JsonConvert.SerializeObject(forms);

            return new ObjectResult(serializedObject);

        }
    }
}
