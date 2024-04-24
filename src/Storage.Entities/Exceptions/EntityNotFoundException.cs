namespace Storage.Entities.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Type entityType, string id) : base($"{entityType} with id {id} not found.") { }
}