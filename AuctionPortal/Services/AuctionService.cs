using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Services;

public class AuctionService : IAuctionService
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;

    public AuctionService(AuctionDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }


    public async Task<AuctionViewModel> CreateAuctionAsync(AuctionViewModel viewModel, CancellationToken cancellationToken = default)
    {
        // Ensure the creator exists in the DB
        var creator = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == viewModel.CreatorId);

        if (creator == null)
        {
            throw new InvalidOperationException("Creator not found in the database.");
        }

        // Map the view model to the entity
        var auctionEntity = _mapper.Map<AuctionModel>(viewModel);

        // Assign the linked ApplicationUser
        auctionEntity.Creator = creator;
        auctionEntity.CreatedAt = DateTime.UtcNow;
        auctionEntity.UpdatedAt = DateTime.UtcNow;

        // Save to DB
        _dbContext.Auctions.Add(auctionEntity);
        await _dbContext.SaveChangesAsync();

        // Return the saved auction mapped back to view model
        return _mapper.Map<AuctionViewModel>(auctionEntity);
    }

    public async Task<IEnumerable<AuctionViewModel>> GetAuctionsAsync(CancellationToken cancellationToken = default)
    {
        var auctionModels = await _dbContext.Auctions
                            .Include(a => a.Image)
                            .Include(a => a.Products)
                                .ThenInclude(p => p.Images)
                            .AsNoTracking().OrderBy(a => a.EndsAt)
                            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<AuctionModel>, IEnumerable<AuctionViewModel>>(auctionModels);
    }
        

    public async Task<AuctionViewModel> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var auctionModel = await _dbContext.Auctions
                           .Include(a => a.Image)
                           .Include(a => a.Products)
                                .ThenInclude(p => p.Images)
                           .AsNoTracking().OrderBy(a => a.EndsAt)
                            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (auctionModel == null)
            throw new Exception($"Auction with Id {id} not found in the database.");

        return _mapper.Map<AuctionModel, AuctionViewModel>(auctionModel);
    }

    public Task UpdateAuctionAsync(AuctionViewModel auctionViewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var auction = await _dbContext.Auctions.FindAsync(id);
        if (auction != null)
        {
            _dbContext.Auctions.Remove(auction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
