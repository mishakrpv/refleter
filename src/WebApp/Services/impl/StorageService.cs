using System.Text.Json.Nodes;

namespace WebApp.Services.Impl;

public sealed class StorageService(HttpClient client)
{
    private readonly HttpClient _client = client;
    
    private const string RemoteServiceBaseUrl = "/api/v1/storage";

    public async Task<ScopeRecord[]> GetScopesByCloudIdAsync(string cloudId)
    {
        var requestUri = $"{RemoteServiceBaseUrl}/scope/by/{cloudId}";
        return (await _client.GetFromJsonAsync<ScopeRecord[]>(requestUri))!;
    }

    public async Task<ScopeRecord> GetScopeAsync(string id)
    {
        var requestUri = $"{RemoteServiceBaseUrl}/scope/{id}";
        return (await _client.GetFromJsonAsync<ScopeRecord>(requestUri))!;
    }

    public async Task<HttpResponseMessage> CreateScopeAsync(string cloudId, string name)
    {
        const string requestUri = $"{RemoteServiceBaseUrl}/scope/create";

        var content = new { Name = name, UserId = cloudId };
        
        return await _client.PostAsJsonAsync(requestUri, content);
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