using Azure;
using Azure.AI.FormRecognizer.Training;
using System;
using System.Threading.Tasks;

namespace trainingModel
{
    class Program
    {
        // For this sample, you can use the training forms found in the `application` folder.
        // Upload the forms to your storage container and then generate a container SAS URL.
        const string TRAINING_FILE_URI = "";
        const string API_ENDPOINT = "";
        const string API_KEY = "";

        static async Task Main(string[] args)
        {
            Uri trainingFileUri = new Uri(TRAINING_FILE_URI);
            string modelName = "JobApplicationModel";
            FormTrainingClient client = new FormTrainingClient(new Uri(API_ENDPOINT), new AzureKeyCredential(API_KEY));

            TrainingOperation operation = await client.StartTrainingAsync(trainingFileUri, useTrainingLabels: true, modelName);
            Response<CustomFormModel> operationResponse = await operation.WaitForCompletionAsync();
            CustomFormModel model = operationResponse.Value;

            Console.WriteLine($"Custom Model Info:");
            Console.WriteLine($"  Model Id: {model.ModelId}");
            Console.WriteLine($"  Model name: {model.ModelName}");
            Console.WriteLine($"  Model Status: {model.Status}");
            Console.WriteLine($"  Is composed model: {model.Properties.IsComposedModel}");
            Console.WriteLine($"  Training model started on: {model.TrainingStartedOn}");
            Console.WriteLine($"  Training model completed on: {model.TrainingCompletedOn}");
        }
    }
}
