using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace AuctionPortal.Services;

public interface IBlobStorageService
{
    Task<ImageViewModel> UploadAsync(IBrowserFile file, string containerName = "auction-images");
    Task<IEnumerable<ImageViewModel>> UploadAsync(IEnumerable<IBrowserFile> files, string containerName = "auction-images");
}

