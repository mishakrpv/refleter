using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Queries;

namespace Storage.API.Controllers;

public sealed class SecretController(IMediator mediator, IQueries queries) : RootController
{
    private readonly IMediator _mediator = mediator;
    private readonly IQueries _queries = queries;

    [HttpPost]
    [Route("")]
    [Route("add")]
    public async Task<IActionResult> AddSecret()
    {
        throw new NotImplementedException();
    }
}