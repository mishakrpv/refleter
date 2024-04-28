using Identity.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddIdentityServer();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseIdentityServer();

app.Run();