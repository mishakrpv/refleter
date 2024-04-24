using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Storage.Application.Commands.UpdateSecret;

public sealed class UpdateSecretRequest : IRequest
{
    [Required]
    public required string ScopeId { get; init; }
    [Required]
    public required string Id { get; init; }
    public required string Value { get; init; }
}