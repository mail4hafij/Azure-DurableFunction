using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FUNCTIONS.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FUNCTIONS
{
    public static class DurableFunction
    {
        [FunctionName("Durable_Orchestration")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();
            // Receiving data from the start (HttpStart)
            OrderRequest req = context.GetInput<OrderRequest>();

            foreach (var order in req.OrderList)
            {
                outputs.Add(await context.CallActivityAsync<string>("Durable_ProcessOrder", order));
                outputs.Add(await context.CallActivityAsync<string>("Durable_SendEmail", order.email));
            }

            return outputs;
        }

        [FunctionName("Durable_ProcessOrder")]
        public static string ProcessOrder([ActivityTrigger] Order order, ILogger log)
        {
            log.LogInformation($"Processing order with amount {order.amount}.");
            return $"order processed {order.reference}!";
        }

        [FunctionName("Durable_SendEmail")]
        public static string SendEmail([ActivityTrigger] string email, ILogger log)
        {
            log.LogInformation($"Sending email to {email}.");
            return $"Email sent!";
        }


        [FunctionName("Durable_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // STARTS HERE
            // Receiving data from the http request.
            var data = await req.Content.ReadAsAsync<OrderRequest>();

            // Calling the Orchestration function as async and passing the request data.
            string instanceId = await starter.StartNewAsync("Durable_Orchestration", data);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}