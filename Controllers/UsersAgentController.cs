using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Filters;
using TurnosLaM.Data;
using TurnosLaM.Models;
using TurnosLaM.Models;
using TurnosLaM.Helpers;
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
        // ----------------- PANEL VIEW:

        // ----------------- CREATE VIEW:
        public async Task<IActionResult> Create()
        {
            return View();
        }
        // ----------------- CREATE ACTION:
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
                //MTO= En esta linea cambio las skills (Hablilidades) de un tipo de dato a otro. en este caso de Bool a String, como nos decia andres ayer (Opreador ternario)
                Skills = $"{(employee.Skill1 ? "Am" :"null")}, {(employee.Skill2 ? "Pf" :"null")},{(employee.Skill3 ? "Inf" : "null")},{(employee.Skill4 ? "Cm" : "null")}"
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



