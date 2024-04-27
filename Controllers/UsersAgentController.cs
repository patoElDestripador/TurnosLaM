using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Filters;
using TurnosLaM.Data;
using TurnosLaM.Models;
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

        // ----------------- CREATE ACTION:
        public IActionResult Create(){
        return View();
}
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployee employee)
        {
            // Se instancia un objeto del modelo 'Employee' para insentar los datos necesarios al modelo:
            Employee NewEmployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Status = employee.Status,
                CreationDate = DateTime.Now
            };
            // Agrega el objeto al modelo:
            _context.Employees.Add(NewEmployee);
            // Guardar los cambios de DbSet en la db:
            await _context.SaveChangesAsync();
            //------------------------------------------------------
            // Se inicializa una variable para encriptar la contraseña utilizando el método 'EncryptPassword' de la clase (TheHelpercito):
            var EncriptedPassword = TheHelpercito.EncryptPassword(employee.Password);
            // Se inicializa una variable con el último usuario creado para asociarlo con el modelo 'User':
            var lastEmployee = await _context.Employees.OrderByDescending(e => e.CreationDate).FirstOrDefaultAsync();
            // Se inicializa una variable para generar el 'UserName' con el método 'GenerateUserName' de la clase (TheHelpercito):
            // string UserName = TheHelpercito.GenerateUserName(employee.FirstName, employee.LastName, employee.PhoneNumber);
            // Se inicializan las variables de las skills con un valor por defecto:
            var Skill_1 = "null";
            var Skill_2 = "null";
            var Skill_3 = "null";
            var Skill_4 = "null";
            // Se confirma si el usuario ha seleccionado el 'Checkbox' para asignarle su valor:
            if (employee.Skill1)
            {
                Skill_1 = "AM";
            }
            if (employee.Skill2)
            {
                Skill_2 = "PF";
            }
            if (employee.Skill3)
            {
                Skill_3 = "INF";
            }
            if (employee.Skill4)
            {
                Skill_4 = "CM";
            }
            // Se instancia un objeto del modelo 'User' para setear la información en el modelo correctamente:
            var user = new User()
            {
                EmployeesId = lastEmployee.Id,
                UserName = employee.FirstName,
                Password = EncriptedPassword,
                Role = employee.Role,
                Status = "LogOut",
                Skills = $"{Skill_1}, {Skill_2}, {Skill_3}, {Skill_4}"
            };
            // Agrega el objeto al modelo:
            _context.Users.Add(user);
            // Guardar los cambios de DbSet en la db:
            await _context.SaveChangesAsync();
            //------------------------------------------------------
            // Redirecciona a la tabla de 'Empleados':
            return RedirectToAction("Employees", "UsersAdmin");
        }
        // ----------------- PANEL VIEW:
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
        // ----------------- PANEL VIEW:
        // public async Task<IActionResult> IndexPrueba()
        // {
        //     // Se trae el siguiente turno en cola:
        //     var queue = await _context.Queues.FirstOrDefaultAsync();
        //     // Se busca el turno que está asociado:
        //     var shift = await _context.Shifts.FindAsync(queue.ShiftId);
        //     // Se busca el usuario que esté asociado con el turno:
        //     var patient = await _context.Patients.FindAsync(shift.PatientId);

        //     // Se confirma si el siguiente turno en cola no ha sido atendido:
        //     if(queue.Status == "En espera")
        //     {
        //         return View(patient);
        //     }



        //     if (queue != null && patient != null && service != null)
        //     {
        //         var Pacientqueue = new Pacientqueue
        //         {
        //             FirstName = patient.FirstName,
        //             LastName = patient.LastName,
        //             Document = patient.Document,
        //             AssignedShift = queue.AssignedShift,
        //             Calls = queue.Calls,
        //             ServiceName = service.ServiceName,
        //             Id = patient.Id
        //         };

        //         return View(Pacientqueue);
        //     }

        //     // Si no se encuentran registros en alguna de las tablas, devolver null o algún otro manejo apropiado
        //     return View(null);
        // }


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









public async Task<IActionResult> Index()
{
 
    var queue = await _context.Queues.FirstOrDefaultAsync();
    var patient = await _context.Patients.FirstOrDefaultAsync();
    var service = await _context.Services.FirstOrDefaultAsync();
    var isRegistered = await IsPatientRegistered(); 

/*      var queue = await _context.Queues.FirstOrDefaultAsync();
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
            IsRegistered = isRegistered ,
            Address= patient.Address,
            PhoneNumber= patient.PhoneNumber,
            Gender= patient.Gender
     
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
public async Task<bool> IsPatientRegistered()
{
    var patient = await _context.Patients.FirstOrDefaultAsync();
    return patient != null; // Si patient no es null, entonces el paciente está registrado
}







[HttpGet]
public IActionResult Createp(){
    return View();
}

[HttpPost]
[Route("Create")]
public IActionResult Createp(Pacientqueue model) {
    if (ModelState.IsValid)
    {
        var patient = new Patient
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Document = model.Document,
            Eps = model.Eps
            
            // Ajusta esto según sea necesario para incluir otros campos
        };

        _context.Patients.Add(patient);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    // Si el modelo no es válido, regresa a la vista con los errores
    return View(model);
}





}}




