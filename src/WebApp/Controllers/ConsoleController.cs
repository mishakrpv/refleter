using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refleter.ServiceDefaults;
using WebApp.Extensions;
using WebApp.Services.Impl;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class ConsoleController(StorageService storageService,
    IConfiguration configuration) : Controller
{
    private readonly StorageService _storageService = storageService;
    private readonly IConfiguration _configuration = configuration;
    
    public async Task<IActionResult> Index()
    {
        var userId = User.Claims.First(c => c.Type == "sub").Value;
        var model = new IndexViewModel
        {
            ConsoleTreeBarViewModel =
            {
                IdentityWebUrl = _configuration.GetRequiredValue("IdentityUrl"),
                Scopes = await _storageService.GetScopesByUserIdAsync(userId)
            }
        };

        return View(model);
    }
}