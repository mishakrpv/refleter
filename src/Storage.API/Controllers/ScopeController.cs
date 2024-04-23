﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Commands.CreateScope;
using Storage.Application.Commands.DeleteScope;
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
        var result = await _mediator.Send(request);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors.First().Message);
    }

    [HttpGet]
    [Route("{id}")]
    [Route("get/{id}")]
    public async Task<IActionResult> GetScope(string id)
    {
        var result = await _queries.GetScope(id);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors.First().Message);
    }

    [HttpDelete]
    [Route("{id}")]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteScope(string id)
    {
        var result = await _mediator.Send(new DeleteScopeRequest(id));

        return result.IsSuccess ? Ok("The scope was successfully deleted.") : NotFound(result.Errors.First().Message);
    }
}