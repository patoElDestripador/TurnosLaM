using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Filters;
using TurnosLaM.Data;
using TurnosLaM.Models;


namespace TurnosLaM.Controllers{
    public class UsersAgentController : Controller
{
    public readonly BaseContext _context;
    public UsersAgentController(BaseContext context)
    {
        _context = context;
    }
    // ----------------- PANEL VIEW:

    // ----------------- CREATE VIEW:
    public async Task<IActionResult> Create()
    {
        return View();
    }

public async Task<IActionResult> Index()
{
    var queue = await _context.Queues.FirstOrDefaultAsync();
    var patient = await _context.Patients.FirstOrDefaultAsync();
    var service = await _context.Services.FirstOrDefaultAsync();

    if (queue != null && patient != null && service != null)
    {
        var Pacientqueue = new Pacientqueue
        {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Document = patient.Document,
            AssignedShift = queue.AssignedShift,
            Calls = queue.Calls,
            ServiceName = service.ServiceName,
            Id = patient.Id

        };

        return View(Pacientqueue);
    }

    // Si no se encuentran registros en alguna de las tablas, devolver null o algún otro manejo apropiado
    return View(null);
}


   
/*         public async Task<ActionResult> MarcarLlamado(int id)
        {
            var assignedShift = await _context.Queues.FindAsync(id); // Encontrar el turno en la base de datos
            if (assignedShift != null)
            {
                TempData["MessageSuccess"] = $"Llamado #{assignedShift.Calls + 1}";

                if (assignedShift.Calls == 2)
                {
                    assignedShift.Status = "En espera";
                    assignedShift.AssignmentTime = DateTime.Now; // Guarda la hora de inicio para el temporizador
                    await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
                }
                else if (assignedShift.Calls >= 3)
                {
                    assignedShift.Status = "Ausente";
                    await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
                }
                else
                {
                    assignedShift.Calls++;
                    await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
                }
            }
            return RedirectToAction("Index"); // Redirigir a la lista de turnos
        }  */

        public async Task<ActionResult> IncrementarCalls(int id)
        {
            var pacient = await _context.Queues.FindAsync(id);
            if (pacient != null)
            {
              
                
                    pacient.Calls = pacient.Calls + 1;
                
                    _context.Update(pacient); // Guardar los cambios
                    await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }



    }



    
}



