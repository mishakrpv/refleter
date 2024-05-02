namespace Refleter.PublicApi.Model;

public sealed class Secret
{
    public Secret(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; private set; }
    public string Value { get; private set; }
}