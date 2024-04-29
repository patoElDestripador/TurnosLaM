using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;


namespace TurnosLaM.Controllers{
    public class UsersController : Controller{

    public readonly BaseContext _context; 
    public UsersController(BaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

        public IActionResult Document()
    {
        return View();
    }
    public IActionResult Services()
    {
        return View();
    }
    public IActionResult ShiftQueue()
    {
        return View( _context.Queues.Where(q => q.Status == "En espera").OrderBy(q => q.CreationDate).Take(10).ToList());
    }

    


}
}




