var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identitydb");
var storageDb = postgres.AddDatabase("storagedb");
var accessControlDb = postgres.AddDatabase("accesscontroldb");

var redis = builder.AddRedis("redis");

var eventBus = builder.AddRabbitMQ("eventbus");

var identityWeb = builder.AddProject<Projects.Identity_Web>("identityweb")
    .WithReference(identityDb)
    .WithReference(eventBus);

var identityApi = builder.AddProject<Projects.Identity_API>("identityapi")
    .WithReference(identityDb);

var storageApi = builder.AddProject<Projects.Storage_API>("storageapi")
    .WithReference(storageDb)
    .WithReference(eventBus);

var accessControlApi = builder.AddProject<Projects.AccessControl_API>("accesscontrolapi")
    .WithReference(accessControlDb);

var webApp = builder.AddProject<Projects.WebApp>("webapp");

var publicApi = builder.AddProject<Projects.Refleter_PublicApi_API>("publicapi")
    .WithReference(redis)
    .WithReference(storageApi)
    .WithReference(accessControlApi);

builder.Build().Run();