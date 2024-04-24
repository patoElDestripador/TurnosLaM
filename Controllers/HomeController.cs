using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosLaM.Models;
using TurnosLaM.Data;
using TurnosLaM.Helpers;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Storage;

namespace TurnosLaM.Controllers;
//[TheGuardcito]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string message = "")
    {
        ViewBag.Message = message;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public readonly BaseContext _context;
    public HomeController(BaseContext context)
    {
        _context = context;
    }
    // ----------------- LOGIN ACTION:
//     [HttpPost]
//     public async Task<IActionResult> SignIn(string userName, string password)
//     {
//         // Se busca el empleado en la base de datos:
//         var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);

//         // Se inicializa las variables de sesión necesarias:
//         HttpContext.Session.SetString("UserId", user.Id.ToString());
//         // HttpContext.Session.SetInt32("EmployeeId", user.EmployeesId);
//         // HttpContext.Session.SetString("UserName", user.UserName);
//         // HttpContext.Session.SetString("Password", user.Password);
//         // HttpContext.Session.SetString("Role", user.Role);
//         // HttpContext.Session.SetString("Module", user.Module);
//         // HttpContext.Session.SetString("Status", user.Skills);
//     }
// }
