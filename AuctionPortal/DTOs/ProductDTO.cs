namespace AuctionPortal.DTOs;

public record ProductDto(
    Guid Id,
    Guid AuctionId,
    string Title,
    string? Description,
    decimal StartingPrice,
    decimal? ReservePrice,
    DateTime? ExpiryDate,
    decimal? FinalBid,
    string? FinalBidderName,
    bool Sold,
    IReadOnlyList<ImageInfoDTO> Images,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
