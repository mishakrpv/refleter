using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Storage.Application.Commands.AddSecret;

public sealed record AddSecretRequest : IRequest
{
    [Required]
    public required string ScopeId { get; init; }
    [Length(1, 100)]
    public required string Name { get; init; }
    [Required]
    public required string Value { get; init; }
}