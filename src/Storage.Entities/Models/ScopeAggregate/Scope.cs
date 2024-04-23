using Storage.Entities.Enums;

namespace Storage.Entities.Models.ScopeAggregate;

public sealed class Scope
{
    private List<Secret> _secrets = [];
    
    public Scope(string userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public ScopeIcon Icon { get; private set; } = new();
    public IEnumerable<Secret> Secrets => _secrets.AsReadOnly();
    
    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void AddSecret(string name, string value)
    {
        _secrets.Add(new Secret(name, value));
    }

    public bool RemoveSecret(string secretId)
    {
        var secret = Secrets.FirstOrDefault(s => s.Id == secretId);

        if (secret is null)
        {
            return false;
        }

        return _secrets.Remove(secret);
    }
}