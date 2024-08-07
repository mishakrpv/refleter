﻿namespace WebApp.ViewModels;

public sealed class IndexViewModel
{
    public string CloudName { get; set; } = null!;
    public ConsoleTreeBarViewModel ConsoleTreeBarViewModel { get; set; } = new();
}