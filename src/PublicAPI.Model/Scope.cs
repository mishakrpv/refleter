namespace PublicAPI.Model;

public sealed class Scope
{
    public Scope(string userId, string name)
    {
        Id = $"{userId}:{name}";
    }
    
    public string Id { get; private set; }
    
    public List<Secret> Secrets = [];
}