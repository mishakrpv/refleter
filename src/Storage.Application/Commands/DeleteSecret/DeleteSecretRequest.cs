using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Storage.Application.Commands.DeleteSecret;

public sealed record DeleteSecretRequest : IRequest
{
    [Required]
    public required string ScopeId { get; init; }
    [Required]
    public required string Id { get; init; }
}