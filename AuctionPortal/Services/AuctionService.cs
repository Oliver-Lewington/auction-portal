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

    public async Task<AuctionViewModel> CreateAuctionAsync(AuctionViewModel auctionViewModel, CancellationToken cancellationToken = default)
    {
        if (auctionViewModel.Id == Guid.Empty)
            auctionViewModel.Id = Guid.NewGuid();

        var auctionModel = _mapper.Map<AuctionViewModel, AuctionModel>(auctionViewModel);

        _dbContext.Auctions.Add(auctionModel);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return auctionViewModel;
    }

    public async Task<IEnumerable<AuctionViewModel>> GetAuctionsAsync(CancellationToken cancellationToken = default)
    {
        var auctionModels = await _dbContext.Auctions
                            .Include(a => a.Products)
                            .AsNoTracking().OrderBy(a => a.EndsAt)
                            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<AuctionModel>, IEnumerable<AuctionViewModel>>(auctionModels);
    }
        

    public async Task<AuctionViewModel> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var auctionModel = await _dbContext.Auctions
                           .Include(a => a.Products)
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

    public async Task DeleteAuctionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var auction = await _dbContext.Auctions.FindAsync(id);
        if (auction != null)
        {
            _dbContext.Auctions.Remove(auction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
