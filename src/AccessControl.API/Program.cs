using AccessControl.API.Apis;
using AccessControl.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddServices();
builder.AddHealthChecks();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGroup("/api/v1/accesscontrol")
    .WithTags("Access Control API")
    .MapAccessControlApi();

app.Run();