using EventBus.Events;

namespace Identity.API.Events;

public sealed record UserCreatedIntegrationEvent : IntegrationEvent
{
    public string UserId { get; }

    public UserCreatedIntegrationEvent(string userId) => UserId = userId;
}