using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using PublicAPI.Infrastructure.Interfaces.Repositories;
using PublicAPI.Model;
using StackExchange.Redis;

namespace PublicAPI.Infrastructure.Implementation.Repositories;

public sealed class RedisScopeRepository(ILogger<RedisScopeRepository> logger, IConnectionMultiplexer redis) : IScopeRepository
{
    private readonly ILogger<RedisScopeRepository> _logger = logger;
    private readonly IDatabase _database = redis.GetDatabase();
    
    private static RedisKey _scopeKeyPrefix = (RedisKey)"/scope/"u8.ToArray();
    private static RedisKey GetScopeKey(string id) => _scopeKeyPrefix.Append(id);
    
    public async Task<Scope> CreateScopeAsync(Scope scope)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(scope, ScopeSerializationContext.Default.Scope);
        await _database.StringSetAsync(GetScopeKey(scope.Id), (RedisValue)json);
        
        return (await GetScopeAsync(scope.Id))!;
    }

    public async Task<Scope?> GetScopeAsync(string id)
    {
        using var data = await _database.StringGetLeaseAsync(GetScopeKey(id));
        
        if (data is null || data.Length == 0)
        {
            return null;
        }
        return JsonSerializer.Deserialize(data.Span, ScopeSerializationContext.Default.Scope);
    }

    public async Task<bool> DeleteScopeAsync(string id)
    {
        return await _database.KeyDeleteAsync(GetScopeKey(id));
    }

    public async Task<Scope?> UpdateScopeAsync(Scope scope)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(scope, ScopeSerializationContext.Default.Scope);
        var created = await _database.StringSetAsync(GetScopeKey(scope.Id), (RedisValue)json);
        
        if (!created)
        {
            _logger.LogInformation("Problem occurred persisting the scope with id: {ScopeId}", scope.Id);
            return null;
        }
        
        _logger.LogInformation("Scope persisted successfully.");
        return await GetScopeAsync(scope.Id);
    }
}

[JsonSerializable(typeof(Scope))]
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
public partial class ScopeSerializationContext : JsonSerializerContext
{

}