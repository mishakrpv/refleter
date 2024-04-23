using MediatR;
using Storage.Application.Queries;

namespace Storage.API.Controllers;

public sealed class SecretController(IMediator mediator, IQueries queries) : RootController
{
    private readonly IMediator _mediator = mediator;
    private readonly IQueries _queries = queries;
}