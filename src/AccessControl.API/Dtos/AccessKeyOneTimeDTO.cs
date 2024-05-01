namespace AccessControl.API.Dtos;

public sealed record AccessKeyOneTimeDTO
{
    public required string Key { get; init; }
    public required string SecretKey { get; init; }
}