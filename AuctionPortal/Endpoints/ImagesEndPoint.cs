using Azure.Storage.Blobs;

namespace AuctionPortal.Endpoints;

public static class ImagesEndpoint
{
    public static void MapImagesEndpoints(this WebApplication app)
    {
        app.MapGet("/images/{blobName}", async (string blobName, BlobServiceClient blobServiceClient) =>
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("auction-images");
            var blobClient = containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
                return Results.NotFound();

            var downloadInfo = await blobClient.DownloadAsync();

            return Results.File(
                downloadInfo.Value.Content,
                contentType: downloadInfo.Value.ContentType ?? "application/octet-stream",
                fileDownloadName: blobName
            );
        });
    }
}
