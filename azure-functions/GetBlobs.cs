using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetBlobs
    {
        private readonly ILogger<GetBlobs> logger;
        private BlobContainerClient blobContainerClient;

        public GetBlobs(ILogger<GetBlobs> logger, IConfiguration configuration)
        {
            this.logger = logger;
            blobContainerClient = AzureBlobStorageConnection.GetBlobContainerClient(configuration);
        }

        [Function("GetBlobs")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string message = "";
            foreach (BlobItem blob in blobContainerClient.GetBlobs()){
                message = message + blob.Name + "\n";
            }

            this.logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(message);
        }
    }
}
