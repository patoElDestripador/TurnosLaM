using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;
using Microsoft.EntityFrameworkCore;


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
    public async Task<IActionResult> ShiftQueue()
    {
        var latestAssignedQueue = await _context.Queues
            .Where(q => q.Status == "En progreso")
            .OrderByDescending(q => q.AssignmentTime)
            .FirstOrDefaultAsync();
            if(latestAssignedQueue != null){
                ViewData["LastShift"] = latestAssignedQueue.AssignedShift ;
            }
        return View(await _context.Queues.Where(q => q.Status == "En espera").OrderBy(q => q.CreationDate).Take(5).ToListAsync());
    }

}
}




