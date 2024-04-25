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
    //Agregamos el aparatado  ChangeSkill (cambiar la skill) Agregamos un nuevo parametro, en este caso la <T> me sirve para Actualizar skills 
        public async Task<IActionResult> ChangeSkill<T>(int? id, T skills)
    {
        //Agregamos la variable para guardar esl id
        var userId =id;
      
        return View(await _context.Users.FindAsync(id));
        //¿Agregamos el cambio de Skill?
        
    }

}
