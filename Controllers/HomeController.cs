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
    public readonly BaseContext _context;

    public HomeController(ILogger<HomeController> logger, BaseContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index(string message = "")
    {
        ViewBag.Message = message;
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    // ----------------- LOGIN ACTION:
    
    [HttpPost]
    public async Task<IActionResult> SignIn(string userName, string password)
    {
        // Se confirma que los campos no estén vacíos:
        if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        {
            // Se busca el empleado en la base de datos:
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            // Se confirma que se haya encontrado un usuario:
            if(user != null)
            {
                // Se inicializa las variables de sesión necesarias:
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                // Se confirma el rol del usuario:
                if(user.Role == "Asesor")
                {
                    // Se redirecciona al panel de asesores:
                    return RedirectToAction("Index", "UsersAgent");
                }
                else
                {
                    // Se redirecciona al panel de MSC:
                    return RedirectToAction("Index", "UsersAdmin");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home", new {message = "¡Usuario no registrado!" });
            }
        }
        else
        {
            return RedirectToAction("Index", "Home", new {message = "¡Llena los campos!"});
        }
    }
    
}
