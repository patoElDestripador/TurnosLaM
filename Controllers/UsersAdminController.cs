using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;

namespace TurnoLaM.Controllers;

public class UsersAdminController : Controller
{
    public readonly BaseContext _context;
    public UsersAdminController(BaseContext context)
    {
        _context = context;
    }
    //Mostramos los botones
    public async Task<IActionResult> Index()
    {
        return View("Index");
    }
    //Mostramos la tabla Shifts (Turnos)
    public async Task<IActionResult> Shifts()
    {
        return View("Shifts");
    }
    //Agregamos la tabla employees (Empleados)
    public async Task<IActionResult> Employees()
    {
        return View("Employees");
    }
    //Agregamos la Tabla Queues (Colas)
    public async Task<IActionResult> Queues()
    {
        return View("Queues");
    }
    //Agregamos la tabla AssingShift (Asignar turno)
    public async Task<IActionResult>AssingShift()
    {
        return View(""); //Agregar a controlador e index de dina (Esta vista es la de Nuestros servicios. donde el paciente escogera el tipo de servicio que necesita)
    }

}   