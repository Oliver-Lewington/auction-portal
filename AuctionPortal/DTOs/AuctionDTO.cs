namespace AuctionPortal.DTOs;

public record AuctionDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime? StartTime,
    DateTime? EndTime,
    bool LiveFlag,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IReadOnlyList<ProductDto> Products
);
