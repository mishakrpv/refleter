using EventBus.Events;

namespace Storage.API.IntegrationEvents.Events;

public sealed record ScopeCreatedIntegrationEvent(string UserId, string ScopeName) : IntegrationEvent;