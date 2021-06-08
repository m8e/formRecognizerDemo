using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Contoso;
using FluentValidation;

namespace function
{
    public class ProcessForm
    {
        [FunctionName("ProcessForm")]
        [return: ServiceBus("processForm", Connection = "StrBusCnx")]
        public async Task<Payload> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();            
            var payload = JsonConvert.DeserializeObject<Payload>(requestBody);
            payload.ModelId = Environment.GetEnvironmentVariable("ModelId");

            var validator = new PayloadValidator();
            validator.ValidateAndThrow(payload);

            return payload;
        }
    }
}
