using MediatR;

namespace AccessControl.API.DI;

public sealed class AccessControlServices(
    IMediator mediator,
    ILogger<AccessControlServices> logger)
{
    public IMediator Mediator { get; } = mediator;
    public ILogger<AccessControlServices> Logger { get; } = logger;
}