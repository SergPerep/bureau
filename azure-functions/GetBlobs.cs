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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            List<string> blobNames = [.. blobContainerClient.GetBlobs().Select(bl => bl.Name)];
            return new OkObjectResult(new { blobNames });
        }
    }
}
