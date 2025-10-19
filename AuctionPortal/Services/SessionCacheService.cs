using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AuctionPortal.Services;

public class SessionCacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<SessionCacheService> _logger;

    public SessionCacheService(IDistributedCache cache, ILogger<SessionCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    private static string GetKey(string sessionId) => $"session:{sessionId}";

    public async Task<Guid?> GetDraftAuctionIdAsync(string sessionId)
    {
        var data = await _cache.GetStringAsync(GetKey(sessionId));
        if (string.IsNullOrEmpty(data)) return null;

        try
        {
            var payload = JsonSerializer.Deserialize<SessionData>(data);
            return payload?.DraftAuctionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize Redis session data for {SessionId}", sessionId);
            return null;
        }
    }

    public async Task SetDraftAuctionIdAsync(string sessionId, Guid draftId)
    {
        var payload = new SessionData { DraftAuctionId = draftId };
        var json = JsonSerializer.Serialize(payload);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5)
        };

        await _cache.SetStringAsync(GetKey(sessionId), json, options);
    }

    public async Task ClearSessionAsync(string sessionId)
    {
        await _cache.RemoveAsync(GetKey(sessionId));
    }

    private class SessionData
    {
        public Guid DraftAuctionId { get; set; }
    }
}
