using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Filters;
using TurnosLaM.Data;
using TurnosLaM.Models;
using TurnosLaM.Helpers;


namespace TurnosLaM.Controllers
{
    public class UsersAgentController : Controller
    {
        public readonly BaseContext _context;
        public UsersAgentController(BaseContext context)
        {
            _context = context;
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
        // ------------------------------------------------------
        public async Task<ActionResult> IncrementarCalls(int id)
        {
            var pacient = await _context.Queues.FindAsync(id);
            if (pacient != null)
            {
                pacient.Calls = pacient.Calls + 1;
                _context.Update(pacient); // Guardar los cambios
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new {idPrueba = id});
        }
        // ------------------------------------------------------
        public async Task<IActionResult> Index(int? idPrueba = null)
        {
            // Inicializa variable con la fecha actual, ignorando la hora:
            DateTime fechaActual = DateTime.Now.Date;

            var queue = new Queue();
            if (idPrueba == null)
            {
                // Consulta LINQ para obtener el turno más antiguo del día actual:
                queue = await _context.Queues
                    .Where(q => q.Status == "En espera" && q.CreationDate.Date == fechaActual)
                    .OrderBy(q => q.CreationDate) // Ordena la hora de creación en orden ascendente
                    .FirstOrDefaultAsync();
            }
            else
            {
                queue = await _context.Queues.FindAsync(idPrueba);
            }
            var patient = await _context.Patients.FindAsync(queue.UserId);
            var shift = await _context.Shifts.FindAsync(queue.ShiftId);
            var service = await _context.Services.FindAsync(shift.ServiceId);
            var isRegistered = await IsPatientRegistered();

            /*  var queue = await _context.Queues.FirstOrDefaultAsync();
                var patient = await _context.Patients.FirstOrDefaultAsync();
                var service = await _context.Services.FirstOrDefaultAsync();
                var isRegistered = await IsPatientRegistered(); */

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
                    Id = patient.Id,
                    Eps = patient.Eps,
                    IsRegistered = isRegistered,
                    Address = patient.Address,
                    PhoneNumber = patient.PhoneNumber,
                    Gender = patient.Gender,
                    QueueId = queue.Id
                    // Agregar la información sobre si el paciente está registrado
                };
                changeStatus(queue, "En progreso");
                return View(Pacientqueue);
            }
            else
            {
                // Si el paciente no está registrado, establecer IsRegistered como false
                var Pacientqueue = new Pacientqueue
                {
                    IsRegistered = false
                };

                return View(Pacientqueue);
            }
        }

        // ------------------------------------------------------
        public async Task<IActionResult> IndexInProgress(int idPrueba)
        {
            Console.WriteLine("------------------------------- ID --------------------------- " + idPrueba);
            var queue = await _context.Queues.FindAsync(idPrueba);
            var patient = await _context.Patients.FindAsync(queue.UserId);
            var shift = await _context.Shifts.FindAsync(queue.ShiftId);
            var service = await _context.Services.FindAsync(shift.ServiceId);
            var isRegistered = await IsPatientRegistered();

            /*  var queue = await _context.Queues.FirstOrDefaultAsync();
                var patient = await _context.Patients.FirstOrDefaultAsync();
                var service = await _context.Services.FirstOrDefaultAsync();
                var isRegistered = await IsPatientRegistered(); */

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
                    Id = patient.Id,
                    Eps = patient.Eps,
                    IsRegistered = isRegistered,
                    Address = patient.Address,
                    PhoneNumber = patient.PhoneNumber,
                    Gender = patient.Gender,
                    QueueId = queue.Id
                    // Agregar la información sobre si el paciente está registrado
                };
                return View(Pacientqueue);
            }
            else
            {
                // Si el paciente no está registrado, establecer IsRegistered como false
                var Pacientqueue = new Pacientqueue
                {
                    IsRegistered = false
                };
                return View(Pacientqueue);
            }
        }
        // ------------------------------------------------------
        private void changeStatus(Queue queue, string status)
        {
            queue.Status = status;
            _context.Queues.Update(queue);
            _context.SaveChanges();
        }
        // ------------------------------------------------------
        public async Task<ActionResult> nextPatient(int id)
        {
            var queue = _context.Queues.Find(id);
            if(queue.Calls == 2)
            {
                changeStatus(queue, "Ausente");
            }
            else
            {
                changeStatus(queue, "Atendida");
            }
            return RedirectToAction("Index");
        }
        // ------------------------------------------------------
        public async Task<bool> IsPatientRegistered()
        {
            var patient = await _context.Patients.FirstOrDefaultAsync();
            return patient != null; // Si patient no es null, entonces el paciente está registrado
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Createp(Pacientqueue model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Document = model.Document,
                    Eps = model.Eps,
                    Gender = model.Gender,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            // Si el modelo no es válido, regresa a la vista con los errores
            return View(model);
        }

        /* -------------------------------------------------  NEXT PATIENT -------------------------------------------------*/
        // public async Task<IActionResult> NextPatient(Queue queue)
        // {
        //     // Inicializa variable con la fecha actual, ignorando la hora:
        //     DateTime fechaActual = DateTime.Now.Date;

        //     // Consulta LINQ para obtener el turno más antiguo del día actual:
        //     var turnoEnEsperaMasAntiguo = await _context.Queues
        //         .Where(q => q.Status == "En espera" && q.CreationDate.Date == fechaActual)
        //         .OrderBy(q => q.CreationDate) // Ordena la hora de creación en orden ascendente
        //         .FirstOrDefaultAsync();
            
        // }
    }
}