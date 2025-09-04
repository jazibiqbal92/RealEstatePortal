using RealEstate.Application.Shared;
using StackExchange.Redis;
using System.Text.Json;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
    {
        await _db.StringSetAsync(key, value, expiry);
        // Store the key in a set for tracking
        await _db.SetAddAsync("properties:keys", key);
    }

    public async Task<string?> GetAsync(string key)
    {
        var value = await _db.StringGetAsync(key);
        return value.HasValue ? value.ToString() : null;
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
        await _db.SetRemoveAsync("properties:keys", key);
    }

    public async Task RemoveByPatternAsync(string patternKeySet)
    {
        var keys = await _db.SetMembersAsync(patternKeySet);
        foreach (var key in keys)
        {
            await _db.KeyDeleteAsync(key.ToString());
        }
        // Clear the set itself
        await _db.KeyDeleteAsync(patternKeySet);
    }
}
