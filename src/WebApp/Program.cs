using Refleter.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.AddDefaultAuthentication();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Console}/{action=Index}");

app.Run();