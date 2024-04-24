using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Commands.AddSecret;
using Storage.Application.Commands.DeleteSecret;
using Storage.Application.Commands.UpdateSecret;
using Storage.Application.Queries;

namespace Storage.API.Controllers;

public sealed class SecretController(IMediator mediator, IQueries queries) : RootController
{
    private readonly IMediator _mediator = mediator;
    private readonly IQueries _queries = queries;

    [HttpPost]
    [Route("")]
    [Route("add")]
    public async Task<IActionResult> AddSecret([FromBody] AddSecretRequest request)
    {
        await _mediator.Send(request);

        return Ok("The secret was successfully added to scope.");
    }
    
    [HttpPut]
    [Route("")]
    [Route("update")]
    public async Task<IActionResult> UpdateSecret([FromBody] UpdateSecretRequest request)
    {
        await _mediator.Send(request);

        return Ok("The secret was successfully updated.");
    }

    [HttpDelete]
    [Route("")]
    [Route("delete")]
    public async Task<IActionResult> DeleteSecret([FromBody] DeleteSecretRequest request)
    {
        await _mediator.Send(request);
        
        return Ok("The secret was successfully deleted.");
    }
}