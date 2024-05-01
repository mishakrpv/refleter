using EventBus.Events;

namespace Storage.API.IntegrationEvents.Events;

public sealed record UserCreatedIntegrationEvent(string UserId) : IntegrationEvent;