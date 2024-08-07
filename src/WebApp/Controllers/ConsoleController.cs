using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refleter.ServiceDefaults;
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var model = new IndexViewModel
        {
            ConsoleTreeBarViewModel =
            {
                IdentityWebUrl = _configuration.GetRequiredValue("IdentityWebUrl"),
                Scopes = await _storageService.GetScopesByUserIdAsync(userId)
            }
        };

        return View(model);
    }
}