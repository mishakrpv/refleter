var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identitydb");

builder.Build().Run();