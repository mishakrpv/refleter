using Refleter.ServiceDefaults;

namespace Identity.Web.Extensions;

public static class AuthenticationExtensions
{
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                var googleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                googleOptions.ClientId = googleAuthSection.GetRequiredValue("ClientId");
                googleOptions.ClientSecret = googleAuthSection.GetRequiredValue("ClientSecret");
            });
    }
}