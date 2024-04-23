namespace Storage.Entities.Models.ScopeAggregate;

public sealed class Secret
{
    public Secret(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Id { get; private set; } = IdHelper.NewStringId();
    public Scope Scope { get; private set; } = null!;
    public string Name { get; private set; }
    public string Value { get; private set; }
    public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;

    public void UpdateValue(string value)
    {
        Value = value;
        LastUpdated = DateTime.UtcNow;
    }
}