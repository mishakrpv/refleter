using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Refleter.PublicApi.Infrastructure.Interfaces.Services;
using Refleter.PublicApi.Model;

namespace Refleter.PublicApi.Infrastructure.Implementation.Services;

public sealed class StorageService(HttpClient client) : IStorageService
{
    private readonly HttpClient _client = client;
    
    private const string RemoteServiceBaseUrl = "api/v1/storage";

    public async Task<Scope?> GetScope(string userId, string name)
    {

        var uri = $"{RemoteServiceBaseUrl}/scope/by/{userId}/{name}";
        return await _client.GetFromJsonAsync<Scope>(uri);
    }
}