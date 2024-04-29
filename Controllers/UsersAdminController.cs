using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System;
using TurnosLaM.Helpers;

namespace TurnoLaM.Controllers;

public class UsersAdminController : Controller
{
    public readonly BaseContext _context;
    private object employee;

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
    //Creamos una vista de usuarios 
    public async Task<IActionResult> Users()
    {
        var users = await _context.Users.ToListAsync();
        List<UserModify> allUsers = new List<UserModify>();;
        foreach (var user in users){
            string[] skills = user.Skills.Split(',');
            UserModify userM = new UserModify(){
                UserName = user.UserName,
                Role = user.Role,
                Module = user.Module,
                Status = user.Status,
                Skills1 = String.IsNullOrEmpty(skills[0]) ? "true" :"false", 
                Skills2 = String.IsNullOrEmpty(skills[1]) ? "true" :"false", 
                Skills3 = String.IsNullOrEmpty(skills[2]) ? "true" :"false", 
                Skills4 = String.IsNullOrEmpty(skills[3]) ? "true" :"false", 
            };
            allUsers.Add(userM);
        };
        ViewData["users"] = allUsers;
        return View ();
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
        //Agregamos la variable para guardar el id de las skills
        var userId =id;
        //Agregamos la condición para cambiar y agregar las skill del UserAgent 
        //Agregar la condicón y agregar los campos de las skills

        return View(await _context.Users.FindAsync(id));
        //¿Agregamos el cambio de Skill?


        
    }

    [HttpPost]
    public IActionResult ChangeSkills(FormCollection form)
    {
        //List<string> lo que hago en esta linea es listar los checkbox que tengo
        List<string>SelectedSkill = new List<string>();
        foreach (string key  in form.Keys) //Iteramos sobre las llaves del formulario donde tengo mis checkbox
        {
            if(form[key] == "on")
            {   //Si la skill es seleccionada la añadimos a la lista de SelectedSkill
                SelectedSkill.Add(key);
            }
        }
        return View("Employess");
        //Implementar Un modelo de manera de actulizar a la base de datos y ¿Una vista o un Sweet alert de confirmación?
    }



     // ----------------- CREATE VIEW:
    public IActionResult Create()
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
            Module = employee.Module,
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
