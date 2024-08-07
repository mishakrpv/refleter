﻿namespace WebApp.Services.Impl;

public sealed class StorageService(HttpClient client)
{
    private readonly HttpClient _client = client;
    
    private const string RemoteServiceBaseUrl = "/api/v1/storage";

    public async Task<ScopeRecord[]> GetScopesByUserIdAsync(string userId)
    {
        var requestUri = $"{RemoteServiceBaseUrl}/scope/by/{userId}";
        return (await _client.GetFromJsonAsync<ScopeRecord[]>(requestUri))!;
    }

    public async Task<ScopeRecord> GetScopeAsync(string id)
    {
        var requestUri = $"{RemoteServiceBaseUrl}/scope/{id}";
        return (await _client.GetFromJsonAsync<ScopeRecord>(requestUri))!;
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