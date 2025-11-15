using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

namespace AuctionPortal.Services
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<AzureBlobStorageService> _logger;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient, ILogger<AzureBlobStorageService> logger)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        public async Task<ImageViewModel> UploadAsync(IBrowserFile file, string containerName = "auction-images")
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobId = Guid.NewGuid();
            var extension = Path.GetExtension(file.Name);
            var blobName = $"{blobId}{extension}";
            
            _logger.LogInformation("Uploading {FileName} to container {ContainerName} as {BlobName}", file.Name, containerName, blobName);

            var blobClient = containerClient.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB max
            await blobClient.UploadAsync(stream, new BlobHttpHeaders
            {
                ContentType = file.ContentType
            });

            return new ImageViewModel(blobId, $"/images/{blobId}{extension}");
        }

        public async Task<IEnumerable<ImageViewModel>> UploadAsync(IEnumerable<IBrowserFile> files, string containerName = "auction-images")
        {
            throw new NotImplementedException();
        }

    }
}