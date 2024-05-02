using AccessControl.API.Apis;
using AccessControl.API.Extensions;
using AccessControl.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddServices();
builder.AddHealthChecks();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGroup("/api/v1/accesscontrol")
    .WithTags("Access Control API")
    .MapAccessControlApi();

app.MapGrpcService<AccessControlService>();

app.Run();