namespace Identity.Web.Extensions;

public static partial class IdentityServerExtensions
{
    public static void AddIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer()
            .AddInMemoryApiScopes(ApiScopes)
            .AddInMemoryClients(Clients);
    }
}