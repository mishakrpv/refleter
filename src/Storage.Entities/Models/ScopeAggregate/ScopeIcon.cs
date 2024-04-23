using Storage.Entities.Enums;

namespace Storage.Entities.Models.ScopeAggregate;

public sealed class ScopeIcon
{
    private static Random _random = new();
        
    public ScopeIcon()
    {
        var array  = Enum.GetValues(typeof(Color));
        Color = (Color)(array.GetValue(_random.Next(array.Length)) ?? Color.White);
    }

    public string ScopeUserId { get; private set; } = null!;
    public string ScopeName { get; private set; } = null!;
    public Scope Scope { get; private set; } = null!;
    public Color Color { get; private set; }
}