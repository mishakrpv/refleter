namespace WebApp.ViewModels;

public class ScopeViewModel
{
    public string ScopeName { get; set; } = null!;
    public ConsoleTreeBarViewModel ConsoleTreeBarViewModel { get; set; } = new();
}