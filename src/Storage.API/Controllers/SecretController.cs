using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Commands.AddSecret;
using Storage.Application.Commands.DeleteSecret;
using Storage.Application.Commands.UpdateSecret;

namespace Storage.API.Controllers;

public sealed class SecretController(IMediator mediator) : RootController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("")]
    [Route("add")]
    public async Task<IActionResult> AddSecret([FromBody] AddSecretRequest request)
    {
        await _mediator.Send(request);

        return Ok(new { message = "The secret was successfully added to scope." });
    }
    
    [HttpPut]
    [Route("")]
    [Route("update")]
    public async Task<IActionResult> UpdateSecret([FromBody] UpdateSecretRequest request)
    {
        await _mediator.Send(request);

        return Ok(new { message = "The secret was successfully updated." });
    }

    [HttpDelete]
    [Route("")]
    [Route("delete")]
    public async Task<IActionResult> DeleteSecret([FromBody] DeleteSecretRequest request)
    {
        await _mediator.Send(request);
        
        return Ok(new { message = "The secret was successfully deleted." });
    }
}