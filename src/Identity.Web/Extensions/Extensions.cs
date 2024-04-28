using Identity.Web.Data;
using Identity.Web.Models;

namespace Identity.Web.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ApplicationDbContext>(Constants.POSTGRES_CONNECTION_NAME);
        
        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();
    }
}