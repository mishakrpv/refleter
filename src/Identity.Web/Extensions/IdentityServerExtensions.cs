using Duende.IdentityServer.Models;

namespace Identity.Web.Extensions;

public static class IdentityServerExtensions
{
    public static void AddIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer()
            .AddInMemoryApiScopes(ApiScopes)
            .AddInMemoryClients(Clients);
    }
    
    private static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new(name: "storage", displayName: "Storage API") 
        };
    
    private static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientId = "webapp",
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "storage" }
            }
        };
}