using Hellang.Middleware.ProblemDetails;
using Refleter.ServiceDefaults;
using Storage.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddServices();
builder.AddDefaultOpenApi();
builder.AddHealthChecks();

var app = builder.Build();

app.UseProblemDetails();

app.UseDefaultOpenApi();
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();