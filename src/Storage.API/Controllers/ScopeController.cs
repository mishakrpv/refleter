using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Commands.CreateScope;
using Storage.Application.Commands.DeleteScope;
using Storage.Application.Commands.UpdateScope;
using Storage.Application.Queries;

namespace Storage.API.Controllers;

public sealed class ScopeController(IMediator mediator, IQueries queries) : RootController
{
    private readonly IMediator _mediator = mediator;
    private readonly IQueries _queries = queries;
    
    [HttpPost]
    [Route("")]
    [Route("create")]
    public async Task<IActionResult> CreateScope([FromBody] CreateScopeRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [Route("{id}")]
    [Route("get/{id}")]
    public async Task<IActionResult> GetScope(string id)
    {
        return Ok(await _queries.GetScope(id));
    }

    [HttpGet]
    [Route("by/{userId}")]
    [Route("get/by/{userId}")]
    public async Task<IActionResult> GetScopesByUserId(string userId)
    {
        return Ok(await _queries.GetScopesByUserId(userId));
    }

    [HttpPut]
    [Route("")]
    [Route("update")]
    public async Task<IActionResult> UpdateScope([FromBody] UpdateScopeRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    [Route("")]
    [Route("delete")]
    public async Task<IActionResult> DeleteScope([FromBody] DeleteScopeRequest request)
    {
        await _mediator.Send(request);

        return Ok(new { message = "The scope was successfully deleted." });
    }
}