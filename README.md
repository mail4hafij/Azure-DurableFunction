# Azure-DurableFunction

Comming soon!


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

## Local setup of Azure Storage Emlulator

The Storage Emulator is available as part of the Microsoft Azure SDK. You can also install the Storage Emulator by using the standalone installer. The Storage Emulator currently runs only on Windows. https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator


If you need a Storage Emulator for Linux, one option is the community maintained, open-source Storage Emulator Azurite. I am using azurerite https://github.com/Azure/Azurite to emulate Azure blob containers locally. I am running Azurite in a docker container. The image for Azurite can be found in dockerhub (https://hub.docker.com/_/microsoft-azure-storage-azurite). Read more about MCR (Microsoft Container Registry) here https://github.com/microsoft/containerregistry


You can run the docker container in either of the following options (I prefer option 1)- 

1. Start with storage on disk and restart automatically after reboot. This one uses c:\Data\azurite to store data (change if needed).

``` docker run -d --restart unless-stopped -p 10000:10000 -p 10001:10001 -v c:/Data/azurite:/data mcr.microsoft.com/azure-storage/azurite ```

2. Start without storage on disk. Data will be lost and must be restarted after reboot.

``` docker run -p 10000:10000 -p 10001:10001 mcr.microsoft.com/azure-storage/azurite ```

3. Start with storage on disk. Data will not be lost, but must be restarted after reboot. This one uses c:\Data\azurite to store data (change if needed).

```  docker run -p 10000:10000 -p 10001:10001 -v c:/Data/azurite:/data mcr.microsoft.com/azure-storage/azurite ``` 
