using EventBus.Events;

namespace WebApp.Events;

public sealed record ScopeCreatedIntegrationEvent : IntegrationEvent
{
    public string UserId { get; }
    
    public string ScopeName { get; }

    public ScopeCreatedIntegrationEvent(string userId, string scopeName)
    {
        UserId = userId;
        ScopeName = scopeName;
    }
}