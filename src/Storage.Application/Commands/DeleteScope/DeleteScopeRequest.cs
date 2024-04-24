using MediatR;

namespace Storage.Application.Commands.DeleteScope;

public sealed record DeleteScopeRequest(string Id) : IRequest;