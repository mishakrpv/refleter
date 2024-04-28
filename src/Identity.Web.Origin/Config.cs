using Duende.IdentityServer.Models;

namespace Identity.Web.Extensions;

public static partial class IdentityServerExtensions
{
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