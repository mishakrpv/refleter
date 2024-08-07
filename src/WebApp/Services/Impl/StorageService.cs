using WebApp.Services.Interfaces;

namespace WebApp.Services.Impl;

public sealed class StorageService(HttpClient client)
{
    private readonly HttpClient _client = client;
    
    private const string RemoteServiceBaseUrl = "/api/v1/storage";

    public async Task<ScopeRecord[]> GetScopesByUserIdAsync(string userId)
    {
        var requestUri = $"{RemoteServiceBaseUrl}/scope/by/{userId}";
        var scopes =  await _client.GetFromJsonAsync<ScopeRecord[]>(requestUri);
        return scopes!;
    }
}

public record ScopeRecord(
    string Id,
    string Name,
    string IconColorHexCode,
    DateTime DateCreated,
    SecretRecord[] Secrets);

public record SecretRecord(
    string Id,
    string Name,
    DateTime LastUpdated);