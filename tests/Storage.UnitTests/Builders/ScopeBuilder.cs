using Storage.Entities.Models.ScopeAggregate;

namespace Storage.UnitTests.Builders;

public sealed class ScopeBuilder : AbstractBuilder<Scope>
{
    private string _userId = "123";
    private string _name = "Cool Scope";

    public ScopeBuilder()
    {
        Entity = InitializeWithDefaults();
    }

    public ScopeBuilder WithSecrets(params Secret[] secrets)
    {
        foreach (var secret in secrets)
        {
            Entity.AddSecret(secret.Name, secret.Value);
        }
        return this;
    }
    
    private Scope InitializeWithDefaults()
    {
        return new Scope(_userId, _name);
    }
}