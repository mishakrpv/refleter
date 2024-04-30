using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ConsoleController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}