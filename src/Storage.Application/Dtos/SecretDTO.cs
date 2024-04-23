namespace Storage.Application.Dtos;

public record SecretDTO
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required DateTime LastUpdated { get; init; }
}