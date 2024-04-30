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
        // ----------------- INCREMENT CALLS:
        public async Task<ActionResult> IncrementarCalls(int id)
        {

            var pacient = await _context.Queues.FindAsync(id);
            if (pacient != null)
            {
                pacient.Calls = pacient.Calls + 1;
                _context.Update(pacient); // Guardar los cambios
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { idPrueba = id });
        }
        // ----------------- INDEX ACTION:
        public async Task<IActionResult> Index(int? idPrueba = null)
        {
            // Inicializa variable con la fecha actual, ignorando la hora:
            DateTime fechaActual = DateTime.Now.Date;
            // Se inicializa una variable tipo Queue:
            var queue = new Queue();
            // Condicional para evaluar si el usuario tiene 'llamados':
            if (idPrueba == null)
            {
                /* Si no tiene llamados se hace la consulta original para hayar el turno en cola más antiguo del día actual */
                // Consulta LINQ para obtener el turno más antiguo del día actual:
                queue = await _context.Queues
                    .Where(q => q.Status == "En espera" && q.CreationDate.Date == fechaActual)
                    .OrderBy(q => q.CreationDate) // Ordena la hora de creación en orden ascendente
                    .FirstOrDefaultAsync();
            }
            else
            {
                // Si el asesor ha realizado llamados, se asigna el mismo paciente:
                queue = await _context.Queues.FindAsync(idPrueba);
            }
            // Condicional confirmando si las variables no son nulas:
            if (queue != null)
            {
                // Se haya el turno relacionado con el 'ShiftId' del turno en cola:
                var shift = await _context.Shifts.FindAsync(queue.ShiftId);
                // Se haya el paciente relacionado con el 'UsersId' del turno en cola:
                var patient = await _context.Patients.FindAsync(shift.PatientId);
                // Se haya el servicio relacionado con el 'ServiceId' del turno en cola:
                var service = await _context.Services.FindAsync(shift.ServiceId);
                // Se inicializa variable confirmando si el paciente está registrado?????????:
                var isRegistered = await IsPatientRegistered();
                // Se instancia un objeto del modelo 'Pacientqueue' para enviar la información a la vista:
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
                };
                // Se cambia el 'Status' del paciente:
                changeStatus(queue, "En progreso");
                // Se retorna la vista con la información del paciente:
                TempData["MessageCall"] = queue.AssignedShift;
                return View(Pacientqueue);
            }
            else
            {
                // Si el paciente no está registrado, establecer IsRegistered como false
                var Pacientqueue = new Pacientqueue
                {
                    IsRegistered = false
                };
                Console.WriteLine(Pacientqueue + "-------------------------------------- MODEL --------------------------------");
               TempData["MessagePacient"] = "No hay pacientes pendientes";
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
            if(status != "En progreso"){
                queue.ClosingTime = DateTime.Now;
            }else{
                queue.AssignmentTime = DateTime.Now;
            }
            queue.Status = status;
            _context.Queues.Update(queue);
            _context.SaveChanges();
        }
        // ------------------------------------------------------
        public async Task<ActionResult> nextPatient(int id)
        {
            var queue = await _context.Queues.FindAsync(id);
            TempData["MessageCall"] = queue.AssignedShift;
            if (queue.Calls == 3)
            {
                changeStatus(queue, "Ausente");
            }
            else
            {
                changeStatus(queue, "Atendida");
            }
            return RedirectToAction("Index");
        }
        // ----------------- CHECKING PATIENT:
        public async Task<bool> IsPatientRegistered()
        {
            var patient = await _context.Patients.FirstOrDefaultAsync();
            return patient != null; // Si patient no es null, entonces el paciente está registrado
        }
        // ----------------- CREATE PATIENT:
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




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Busca el usuario en la base de datos y actualiza solo los campos necesarios
                    var existingUser = await _context.Users.FindAsync(id);
                    if (existingUser != null)
                    {
                        existingUser.Status = user.Status; // Actualiza el estado del usuario
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


    }








}




