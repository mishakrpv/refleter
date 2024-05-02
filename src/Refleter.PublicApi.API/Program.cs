using Refleter.PublicApi.API.Apis;
using Refleter.PublicApi.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddServices();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGroup("api/v1/")
    .WithTags("Public API")
    .MapPublicApi();

app.Run();