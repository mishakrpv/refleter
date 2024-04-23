var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identitydb");
var storageDb = postgres.AddDatabase("storagedb");

var identityWeb = builder.AddProject<Projects.Identity_Web>("identityweb")
    .WithReference(identityDb);

var storageApi = builder.AddProject<Projects.Storage_API>("storageapi")
    .WithReference(storageDb);

builder.Build().Run();