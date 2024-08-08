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

// var identityWeb = builder.AddProject<Projects.Identity_Web>("identityweb", launchProfileName)
//     .WithExternalHttpEndpoints()
//     .WithReference(identityDb)
//     .WithReference(eventBus);

var identityApi = builder.AddProject<Projects.Identity_API>("identityapi", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb)
    .WithReference(eventBus);

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);
// var identityWebEndpoint = identityWeb.GetEndpoint(launchProfileName);

var storageApi = builder.AddProject<Projects.Storage_API>("storageapi")
    .WithReference(storageDb)
    .WithReference(eventBus)
    .WithEnvironment("Identity__Url", identityEndpoint);

var accessControlApi = builder.AddProject<Projects.AccessControl_API>("accesscontrolapi")
    .WithReference(accessControlDb)
    .WithEnvironment("Identity__Url", identityEndpoint);

var webApp = builder.AddProject<Projects.WebApp>("webapp", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(storageApi)
    .WithEnvironment("IdentityUrl", identityEndpoint);

var publicApi = builder.AddProject<Projects.Refleter_PublicApi_API>("publicapi")
    .WithReference(redis)
    .WithReference(storageApi)
    .WithReference(accessControlApi);

// Wire up the callback urls (self referencing)
webApp.WithEnvironment("CallBackUrl", webApp.GetEndpoint(launchProfileName));

// Identity has a reference to all the apps for callback urls, this is a cyclic reference
identityApi.WithEnvironment("StorageApiClient", storageApi.GetEndpoint("http"))
    .WithEnvironment("WebAppClient", webApp.GetEndpoint(launchProfileName));

builder.Build().Run();
return;

static bool ShouldUseHttpForEndpoints()
{
    const string envVarName = "REFLETER_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(envVarName);

    return int.TryParse(envValue, out var result) && result == 1;
}