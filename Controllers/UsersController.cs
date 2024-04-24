using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;


namespace TurnosLaM.Controllers{
    public class UsersController : Controller{
    public IActionResult Index()
    {
        return View();
    }

        public IActionResult Documento()
    {
        return View();
    }
    public IActionResult Servicios()
    {
        return View();
    }
        public IActionResult ShiftQueue()
    {
        return View();
    }


}
}




