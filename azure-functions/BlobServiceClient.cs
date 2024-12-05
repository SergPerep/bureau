
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Company.Function
{
    public class AzureBlobStorageConnection
    {
        static public BlobContainerClient GetBlobContainerClient(IConfiguration configuration){
            string? sharedKey = configuration["SharedKey"];
            string accountName = "bureaustore";
            Uri uri = new Uri("https://bureaustore.blob.core.windows.net/bureau");
            StorageSharedKeyCredential credential = new StorageSharedKeyCredential(accountName, sharedKey);
            return new BlobContainerClient(uri, credential);
        }
    }
}