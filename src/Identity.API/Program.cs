using Identity.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddServices();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseIdentityServer();

app.Run();