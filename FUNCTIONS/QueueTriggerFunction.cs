using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace FUNCTIONS
{
    public static class QueueTriggerFunction
    {
        // IMPORTANT: There are 3 ways we can get the storage account connection string
        // Visit https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-queue-trigger?tabs=csharp
        // WAY1: Using param [QueueTriggerFunction("helloworld", Connection = "StorageConnectionAppSetting")]string myQueueItem
        // WAY2: Using storage account attribute on the class level or function level.

        [FunctionName("QueueTriggerFunction")]
        public static void Run([QueueTrigger("helloworld", Connection = "AzureWebJobsStorage")]string myQueueItem,
            ExecutionContext context, ILogger log)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true) // This gives you access to your application settings in your local development environment
                .AddEnvironmentVariables() // This is what actually gets you the application settings in Azure
                .Build();

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
