using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Company.Function
{
    public class GetBlobProps
    {
        private readonly ILogger<GetBlobs> logger;
        private BlobContainerClient blobContainerClient;

        public GetBlobProps(ILogger<GetBlobs> logger, IConfiguration configuration)
        {
            this.logger = logger;
            blobContainerClient = AzureBlobStorageConnection.GetBlobContainerClient(configuration);
        }

        [Function("GetBlobProps")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string? blobName = req.Query.Keys.Contains("blobName") ? req.Query["blobName"] : "";
            if (string.IsNullOrWhiteSpace(blobName)) return new NotFoundResult();
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            BlobProperties props = blobClient.GetProperties();
            return new OkObjectResult(new {url = blobClient.Uri.ToString(), metadata = props.Metadata });
        }
    }
}