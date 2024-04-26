using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;
using Microsoft.EntityFrameworkCore;

namespace TurnoLaM.Controllers;

public class UsersAdminController : Controller
{
    public readonly BaseContext _context;
    public UsersAdminController(BaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View("Index"); //Cuando tenga problemas que aparentemente no son problemas, detener el Endpoint y eliminar el bing
    }

    //Mostramos la tabla Shifts (Turnos)
    public IActionResult Shifts()
    {
        return View( _context.Shifts.ToList()); //Cuando tenga problemas que aparentemente no son problemas, detener el Endpoint y eliminar el bing
    }
    //Agregamos la tabla employees (Empleados)
    public async Task<IActionResult> Employees()
    {
        return View(await _context.Employees.ToListAsync());
    }
    //Agregamos la Tabla Queues (Colas)
    public IActionResult Queues()
    {
        return View(_context.Queues.ToList());
    }

    //Creamos la vista para cambiar skill
 
    //Agregamos el aparatado  ChangeSkill (cambiar la skill) Agregamos un nuevo parametro, en este caso la <T> me sirve para Actualizar skills 
        public async Task<IActionResult> ChangeSkill<T>(int? id, T skills)
    {
        //Agregamos la variable para guardar el id
        var userId =id;
        //Agregamos la condición para cambiar y agregar las skill del UserAgent 
        //Agregar la condicón y agregar los campos de las skills

      
        return View(await _context.Users.FindAsync(id));
        //¿Agregamos el cambio de Skill?


        
    }

}
