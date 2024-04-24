using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;

namespace TurnosLaM.Controllers;
//[TheGuardcito]
public class UsersController : Controller
{

    public IActionResult ShiftQueue()
    {
        return View();
    }

}
