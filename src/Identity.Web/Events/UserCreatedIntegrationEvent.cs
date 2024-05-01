using EventBus.Events;

namespace Identity.Web.Events;

public sealed record UserCreatedIntegrationEvent : IntegrationEvent
{
    public string UserId { get; }

    public UserCreatedIntegrationEvent(string userId) => UserId = userId;
}