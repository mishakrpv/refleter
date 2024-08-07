using Microsoft.AspNetCore.Mvc;
using Refleter.ServiceDefaults;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ConsoleController(IConfiguration configuration) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    
    public IActionResult Index()
    {
        var model = new IndexViewModel
        {
            ConsoleTreeBarViewModel =
            {
                IdentityWebUrl = _configuration.GetSection("Identity").GetRequiredValue("Web_Url")
            }
        };

        return View(model);
    }
}