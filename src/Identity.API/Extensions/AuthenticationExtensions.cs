using Refleter.ServiceDefaults;

namespace Identity.API.Extensions;

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
            })
            .AddGitHub(gitHubOptions =>
            {
                var gitHubAuthSection = builder.Configuration.GetSection("Authentication:GitHub");
                gitHubOptions.ClientId = gitHubAuthSection.GetRequiredValue("ClientId");
                gitHubOptions.ClientSecret = gitHubAuthSection.GetRequiredValue("ClientSecret");
            })
            .AddTwitter(twitterOptions =>
            {
                var twitterAuthSection = builder.Configuration.GetSection("Authentication:Twitter");
                twitterOptions.ConsumerKey = twitterAuthSection.GetRequiredValue("ConsumerKey");
                twitterOptions.ConsumerSecret = twitterAuthSection.GetRequiredValue("ConsumerSecret");
            });
    }
}