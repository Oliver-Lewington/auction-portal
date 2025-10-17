using System;

namespace AuctionPortal.DTOs;

public record ImageInfoDTO(
    Guid Id,
    string Url,
    string? Alt,
    string? Caption
);
