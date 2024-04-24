using System.ComponentModel.DataAnnotations;
using MediatR;
using Storage.Application.Dtos;

namespace Storage.Application.Commands.UpdateScope;

public sealed record UpdateScopeRequest : IRequest<ScopeDTO>
{
    [Required]
    public required string Id { get; init; }
    [Length(3, 100)]
    public required string Name { get; init; }
}