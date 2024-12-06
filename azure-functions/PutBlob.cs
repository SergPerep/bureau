using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Company.Function
{
    public class PutBlob
    {
        private readonly ILogger<GetBlobs> logger;
        private BlobContainerClient blobContainerClient;

        public PutBlob(ILogger<GetBlobs> logger, IConfiguration configuration)
        {
            this.logger = logger;
            blobContainerClient = AzureBlobStorageConnection.GetBlobContainerClient(configuration);
        }

        [Function("PutBlob")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequest req)
        {
            string? blobName = req.Query.Keys.Contains("blobName") ? req.Query["blobName"] : "";
            if (string.IsNullOrWhiteSpace(blobName)) return new NotFoundResult();
            logger.LogTrace($"blobName: {blobName}");
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await req.Body.CopyToAsync(memoryStream);
                    byte[] fileData = memoryStream.ToArray();
                    if (fileData == null || fileData.Length == 0) return new BadRequestObjectResult("No binary data received.");

                    using (var stream = new MemoryStream(fileData))
                    {
                        await blobClient.UploadAsync(stream, overwrite: true);
                    }

                }
                logger.LogInformation($"{blobName} has been uploaded.");
                return new OkObjectResult($"{blobName} has been uploaded.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to upload blob: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}