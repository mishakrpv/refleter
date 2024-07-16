using System.Net.Http.Json;
using System.Text.Json;
using Asp.Versioning;
using Asp.Versioning.Http;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Storage.Application.Commands.CreateScope;
using Storage.Application.Dtos;
using Xunit;

namespace Storage.IntegrationTests;

public sealed class StorageApiTests : IClassFixture<StorageApiFixture>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public StorageApiTests(StorageApiFixture fixture)
    {
        var handler = new ApiVersionHandler(new QueryStringApiVersionWriter(), new ApiVersion(1.0));

        _webApplicationFactory = fixture;
        _httpClient = _webApplicationFactory.CreateDefaultClient(handler);
    }

    [Fact]
    public async Task CreateScope()
    {
        // Arrange

        const string userId = "123";
        const string scopeName = "Scope";
        
        var request = new CreateScopeRequest
        {
            UserId = userId,
            Name = scopeName
        };
        
        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/v1/storage/Scope/create", request);
        response.EnsureSuccessStatusCode();
        
        // Assert
        var body = await response.Content.ReadAsStringAsync();
        var createdScope = JsonSerializer.Deserialize<ScopeDTO>(body, _jsonSerializerOptions);

        createdScope.Should().NotBeNull();
        createdScope.Name.Should().Be(scopeName);
        createdScope.Id.Should().NotBeEmpty();
    }
}