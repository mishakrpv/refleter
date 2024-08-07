﻿using WebApp.Services.Impl;

namespace WebApp.ViewModels;

public sealed class ConsoleTreeBarViewModel
{
    public string IdentityWebUrl { get; set; } = null!;
    public ScopeRecord[] Scopes { get; set; } = null!;
}