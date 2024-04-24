using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Filters;
using TurnosLaM.Data;

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
}
