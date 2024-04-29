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
    public async Task<IActionResult> Privacy()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    // ----------------- LOGIN VIEW:
    public IActionResult Index(string message = "")
    {
        ViewBag.Message = message;
        return View();
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
                // Se inicializa una variable para confirmar si la contraseña proporcionada coincide con la encriptada en la base de datos:
                var passwordMatch = TheHelpercito.VerifyPassword(user.Password, password);
                // Se confirma si coincidió:
                if(passwordMatch)
                {
                    // Se inicializa las variables de sesión necesarias:
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    // Se modifica el 'Status' del empleado:
                    user.Status = "LogIn";
                    // Se actualiza el 'Status' del empleado en la DB:
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
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
                    return RedirectToAction("Index", "Home", new {message = "¡Los datos ingresados no coinciden!" });
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

    // ----------------- LOGOUT ACTION:
    public async Task<IActionResult> Logout()
    {
        // Se obtiene el 'Id' del empleado que inició sesión y se castea a tipo 'Int32' para buscar en la base de datos el 'Id':
        // var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        var userId = int.Parse(HttpContext.Session.GetString("UserId"));
        // Se busca el empleado en la DB:
        var userToUpdate = await _context.Users.FindAsync(userId);
        // Se modifica el 'Status' del empleado:
        userToUpdate.Status = "LogOut";
        // Se actualiza el 'Status' del empleado en la DB:
        _context.Users.Update(userToUpdate);
        await _context.SaveChangesAsync();
        // Se remueven las variables de sesión:
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Index");
    }

    // ----------------- DELETE ACTION:
    // 4. ELIMINAR:
    // public async Task<IActionResult> Eliminar(int id)
    // {
    //     // Se inicializa una variable con la variable de sesión:
    //     var SessionId = Int32.Parse(HttpContext.Session.GetString("EmpleadoId"));
    //     // Se busca el registro que coincida con el 'id' en el modelo 'empleado':
    //     var empleado = await _context.Empleados.FindAsync(id);
    //     // Una vez encontrado, se elimina ese registro del modelo:
    //     _context.Empleados.Remove(empleado);
    //     // Se actualiza la base de datos:
    //     await _context.SaveChangesAsync();
    //     // Se confirma si el usuario eliminado es el mismo que está en la variable de sesión:
    //     if(id == SessionId)
    //     {
    //         // Si es así, se eliminan las variables de sesión (Logout action):
    //         HttpContext.Session.Remove("Email");
    //         HttpContext.Session.Remove("Nombre");
    //         HttpContext.Session.Remove("EmpleadoId");
    //         return RedirectToAction("Index", "Empleados");
    //     }
    //     else
    //     {
    //         return RedirectToAction("PanelEmployees", "Empleados");
    //     }
    // }
}
