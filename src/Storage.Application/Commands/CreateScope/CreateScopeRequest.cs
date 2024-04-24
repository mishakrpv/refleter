using System.ComponentModel.DataAnnotations;
using MediatR;
using Storage.Application.Dtos;

namespace Storage.Application.Commands.CreateScope;

public sealed record CreateScopeRequest : IRequest<ScopeDTO>
{
    [Required]
    public required string UserId { get; init; }
    [Length(3, 100)]
    public required string Name { get; init; }
}