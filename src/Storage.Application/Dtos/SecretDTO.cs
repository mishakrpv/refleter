namespace Storage.Application.Dtos;

public record SecretDTO
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required DateTime LastUpdated { get; init; }
}