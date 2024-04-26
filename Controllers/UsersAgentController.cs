using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Filters;
using TurnosLaM.Data;
using TurnosLaM.Models;
using TurnosLaM.Helpers;

namespace TurnosLaM.Controllers;

public class UsersAgentController : Controller
{
    public readonly BaseContext _context;
    public UsersAgentController(BaseContext context)
    {
        _context = context;
    }
    // ----------------- PANEL VIEW:
    public async Task<IActionResult> Index()
    {
        return View();
    }
    // ----------------- CREATE VIEW:
    public async Task<IActionResult> Create()
    {
        return View();
    }
    // ----------------- CREATE ACTION:
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployee employee){
        // Se instancia un objeto del modelo 'Employee' para insentar los datos necesarios al modelo:
        Employee NewEmployee = new Employee(){
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Status = employee.Status,
            CreationTime = DateTime.Now
        };
        // Agrega el objeto al modelo:
        _context.Employees.Add(NewEmployee);
        // Guardar los cambios de DbSet en la db:
        await _context.SaveChangesAsync();
        //------------------------------------------------------
        // Se inicializa una variable para encriptar la contraseña utilizando el método 'EncryptPassword' de la clase (TheHelpercito):
        var EncriptedPassword = TheHelpercito.EncryptPassword(employee.Password);
        // Se inicializa una variable con el último usuario creado para asociarlo con el modelo 'User':
        var lastEmployee = await _context.Employees.OrderByDescending(e => e.CreationTime).FirstOrDefaultAsync();
        // Se inicializa una variable para generar el 'UserName' con el método 'GenerateUserName' de la clase (TheHelpercito):
        // string UserName = TheHelpercito.GenerateUserName(employee.FirstName, employee.LastName, employee.PhoneNumber);
        // Se inicializan las variables de las skills con un valor por defecto:
        var Skill_1 = "null";
        var Skill_2 = "null";
        var Skill_3 = "null";
        var Skill_4 = "null";
        // Se confirma si el usuario ha seleccionado el 'Checkbox' para asignarle su valor:
        if(employee.Skill1)
        {
            Skill_1 = "AM";
        }
        if(employee.Skill2)
        {
            Skill_2 = "PF";
        }
        if(employee.Skill3)
        {
            Skill_3 = "INF";
        }
        if(employee.Skill4)
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
}
