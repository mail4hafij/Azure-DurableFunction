using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace FUNCTIONS
{
    public static class TimeTriggerFunction
    {
        // IMPORTANT.
        // To know how to access app settings like other projects, please visit
        // https://www.koskila.net/how-to-access-azure-function-apps-settings-from-c/


        [FunctionName("TimeTriggerFunction")]
        public static void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, 
            ExecutionContext context, ILogger log)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true) // This gives you access to your application settings in your local development environment
                .AddEnvironmentVariables() // This is what actually gets you the application settings in Azure
                .Build();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
