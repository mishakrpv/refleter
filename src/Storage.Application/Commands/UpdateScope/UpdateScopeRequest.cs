using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;
using Storage.Application.Dtos;

namespace Storage.Application.Commands.UpdateScope;

public sealed record UpdateScopeRequest : IRequest<Result<ScopeDTO>>
{
    public required string Id { get; init; }
    [Length(3, 100)]
    public required string Name { get; init; }
}