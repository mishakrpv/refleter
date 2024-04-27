var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identitydb");
var storageDb = postgres.AddDatabase("storagedb");
var accessControlDb = postgres.AddDatabase("accesscontroldb");

var messaging = builder.AddKafka("messaging");

var identityWeb = builder.AddProject<Projects.Identity_Web>("identityweb")
    .WithReference(identityDb)
    .WithReference(messaging);

var storageApi = builder.AddProject<Projects.Storage_API>("storageapi")
    .WithReference(storageDb);

var accessControlApi = builder.AddProject<Projects.AccessControl_API>("accesscontrolapi")
    .WithReference(accessControlDb);

builder.Build().Run();