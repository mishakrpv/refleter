using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Storage.IntegrationTests;

public sealed class StorageApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly IHost _app;
    
    public IResourceBuilder<PostgresServerResource> Postgres { get; private set; }
    public IResourceBuilder<PostgresDatabaseResource> StorageDb { get; private set; }
    public IResourceBuilder<PostgresDatabaseResource> IdentityDb { get; private set; }
    public IResourceBuilder<ProjectResource> IdentityApi { get; private set; }
    private string _postgresConnectionString;

    public StorageApiFixture()
    {
        var options = new DistributedApplicationOptions
        {
            AssemblyName = typeof(StorageApiFixture).Assembly.FullName,
            DisableDashboard = true
        };

        var appBuilder = DistributedApplication.CreateBuilder(options);

        Postgres = appBuilder.AddPostgres("postgres");
        StorageDb = Postgres.AddDatabase("storagedb");
        IdentityDb = Postgres.AddDatabase("identitydb");
        IdentityApi = appBuilder.AddProject<Projects.Identity_API>("identityapi").WithReference(IdentityDb);
        _app = appBuilder.Build();
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { $"ConnectionStrings:{StorageDb.Resource.Name}", _postgresConnectionString },
                { "Identity:Url", IdentityApi.GetEndpoint("http").Url }
            }!);
        });
        return base.CreateHost(builder);
    }
    
    public async Task InitializeAsync()
    {
        await _app.StartAsync();
        _postgresConnectionString = await StorageDb.Resource.ConnectionStringExpression.GetValueAsync(default);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await _app.StopAsync();
        if (_app is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            _app.Dispose();
        }
    }
}