using Refleter.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identitydb");
var storageDb = postgres.AddDatabase("storagedb");
var accessControlDb = postgres.AddDatabase("accesscontroldb");

var redis = builder.AddRedis("redis");

var eventBus = builder.AddRabbitMQ("eventbus");

var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

var identityWeb = builder.AddProject<Projects.Identity_Web>("identityweb", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb)
    .WithReference(eventBus);

var identityApi = builder.AddProject<Projects.Identity_API>("identityapi", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);
var identityWebEndpoint = identityWeb.GetEndpoint(launchProfileName);

var storageApi = builder.AddProject<Projects.Storage_API>("storageapi")
    .WithReference(storageDb)
    .WithReference(eventBus)
    .WithEnvironment("Identity__Url", identityEndpoint);

var accessControlApi = builder.AddProject<Projects.AccessControl_API>("accesscontrolapi")
    .WithReference(accessControlDb)
    .WithEnvironment("Identity__Url", identityEndpoint);

var webApp = builder.AddProject<Projects.WebApp>("webapp", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithEnvironment("Identity__Web_Url", identityWebEndpoint)
    .WithEnvironment("Identity__Url", identityEndpoint);

var publicApi = builder.AddProject<Projects.Refleter_PublicApi_API>("publicapi")
    .WithReference(redis)
    .WithReference(storageApi)
    .WithReference(accessControlApi);

builder.Build().Run();

static bool ShouldUseHttpForEndpoints()
{
    const string envVarName = "REFLETER_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(envVarName);

    return int.TryParse(envValue, out var result) && result == 1;
}