var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identitydb");

var identityWeb = builder.AddProject<Projects.Identity_Web>("identityweb")
    .WithReference(identityDb);

builder.Build().Run();