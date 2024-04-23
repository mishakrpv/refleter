namespace Storage.Application.Dtos;

public record ScopeDTO
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string IconColorHexCode { get; init; }
    public required DateTime DateCreated { get; init; }
    public required IEnumerable<SecretDTO> Secrets { get; init; }
}