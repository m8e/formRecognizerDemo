using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Azure.AI.FormRecognizer;  
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;
using Azure;
using System;
using FluentValidation;

[assembly: FunctionsStartup(typeof(Contoso.Startup))]

namespace Contoso
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            var credential = new AzureKeyCredential(Environment.GetEnvironmentVariable("FormRecognizerApiKey"));
            var client = new FormRecognizerClient(new Uri(Environment.GetEnvironmentVariable("FormRecognizerEndpoint")),credential);

            builder.Services.AddSingleton(client);            
        }
    }
}