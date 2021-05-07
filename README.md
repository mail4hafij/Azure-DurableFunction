# Azure-DurableFunction
The idea is to orchestrate functions from a http trigger. Let's say, we have some orders to process. We recieve a http request with multiple orders in json format. We then process each order in async function and send email to the payee in another async function.

## How to run locally
In this solution I am using Storage Emulator that comes with Azure SDK. Although the Storage Emulator is available as part of the Microsoft Azure SDK, you can also install the Storage Emulator by using the standalone installer. The Storage Emulator currently runs only on Windows. https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator

After running the project, use postman to post a http request with the following json data

``` 
{
   "OrderList" : [
        {
            "id" : 1,
            "email" : "test@email.com",
            "reference" : "OCR",
            "amount" : "99.9"
        },
        {
            "id" : 2,
            "email" : "anothertest@email.com",
            "reference" : "OCR",
            "amount" : "199.9"
        }
   ]
}
``` 

Debug away in DurableFunction to see how it is implemented. Checkout the postman response for the orchestration statuses. Try putting large amount of OrderList in the json data.

I have added two extra functions just for testing purposes.
1. QueueTriggerFunction: You can test this by opening your local storage explorer, then add a queue ```helloworld``` and add some msg. This will trigger this function to run.
2. TimeTriggerFunction: It runs every minute.


## Application patterns
1. Function chaining
2. Fan-out/fan-in
3. Async HTTP APIs
4. Monitoring
5. Human interaction
6. Aggregator (stateful entities)

Read more https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview

## Durable Functions type
1. Orchestrator functions
2. Activity functions
3. Entity functions
4. Client functions

Read more https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-types-features-overview

## Storage Emlulator using Azurite
If you need a Storage Emulator for Linux, one option is the community maintained, open-source Storage Emulator Azurite https://github.com/Azure/Azurite. You can run Azurite in a docker container. The image for Azurite can be found in dockerhub https://hub.docker.com/_/microsoft-azure-storage-azurite. Read more about MCR (Microsoft Container Registry) here https://github.com/microsoft/containerregistry

You can run the docker container in either of the following options - 

1. Start with storage on disk and restart automatically after reboot. This one uses c:\Data\azurite to store data.

``` docker run -d --restart unless-stopped -p 10000:10000 -p 10001:10001 -p 10002:10002 -v c:/Data/azurite:/data mcr.microsoft.com/azure-storage/azurite ```

2. Start without storage on disk. Data will be lost and must be restarted after reboot.

``` docker run -p 10000:10000 -p 10001:10001 mcr.microsoft.com/azure-storage/azurite ```

3. Start with storage on disk. Data will not be lost, but must be restarted after reboot. This one uses c:\Data\azurite to store data.

```  docker run -p 10000:10000 -p 10001:10001 -v c:/Data/azurite:/data mcr.microsoft.com/azure-storage/azurite ``` 
