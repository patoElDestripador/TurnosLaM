using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Web;

namespace TurnosLaM.Controllers
{
    public class QueuesController : Controller
    {
        private readonly BaseContext _context;

        public QueuesController(BaseContext context)
        {
            _context = context;


        }

    public async Task<IActionResult> Index()
    {
       var queue = await _context.Queues.FirstOrDefaultAsync(); // Obtener el primer turno de la base de datos
        return View();
    }
            
        public async Task<ActionResult> MarcarLlamado(int id)
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
            return RedirectToAction("Queues"); // Redirigir a la lista de turnos
        }
    }
}


    


    



//[TheGuardacito]

