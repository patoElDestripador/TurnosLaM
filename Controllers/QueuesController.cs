using System.Diagnostics;
using TurnosLaM.Filters;
using Microsoft.AspNetCore.Mvc;
using TurnosLaM.Models;
using System;
using Microsoft.AspNetCore.Authentication;
using TurnosLaM.Data;
using Microsoft.EntityFrameworkCore;
using TurnosLaM.Helpers;

namespace TurnosLaM.Controllers;
//[TheGuardcito]
public class QueuesController : Controller
{

    public readonly BaseContext _context; 
    public QueuesController(BaseContext context)
    {
        _context = context;
    }

    public IActionResult ShiftQueue()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveDocument(string document){
        Patient patient = await _context.Patients.FirstOrDefaultAsync( u => u.Document.Equals(document));

        if(patient != null){
            HttpContext.Session.SetString("patientid", patient.Id.ToString());
            return RedirectToAction("Services","Users");
        }
        HttpContext.Session.SetString("patienDocument", document);
        TempData["documentNotFoundNotify"] = document;
        return RedirectToAction("Document","Users");
        //notificar y enviar info de continuar documentNotFound()
    }
    public async Task<IActionResult> AssignShift(string? serviceName)
    {
        string id = HttpContext.Session.GetString("patientid");
        Service service = await _context.Services.FirstOrDefaultAsync( u => u.ServiceName.Equals(serviceName));
        Patient patient = await _context.Patients.FindAsync(int.Parse(id));
        if(!String.IsNullOrEmpty(patient.Document)){
            ShiftModel shiftModel = new ShiftModel(){
                PatientId = patient.Id,
                ServiceId = service.Id,
                CreationDate = DateTime.Now,
            };
            await _context.Shifts.AddAsync(shiftModel);
            await _context.SaveChangesAsync();
            var SearchUsers = _context.Users.AsQueryable();
            SearchUsers = SearchUsers.Where(u => u.Skills.ToLower().Contains(serviceName) && u.Status == "LogIn");
            var availableUsers = SearchUsers.ToList();
            Queue queueModel = new Queue(){
                UserId = null,
                ShiftId = shiftModel.Id,
                Status = "Por reasignar",
                AssignedShift = await GenerateShift(service.ServiceName),
                CreationDate = DateTime.Now,
                AssignmentTime = null,
                ClosingTime = null,
                Calls = 0
            };
            if (availableUsers.Any())
            {
                var userToAssign = availableUsers.FirstOrDefault();
                queueModel.UserId = userToAssign.Id;
                queueModel.Status = "En espera";
            }
                //se llama a js para que valide con el msc 
                TempData["shiftNotify"] = queueModel.AssignedShift;
                await _context.Queues.AddAsync(queueModel);
                await _context.SaveChangesAsync();
        }else{
            //listar turnos en turnero
        }
        
        return RedirectToAction("Document","Users");
    }

    public async Task<string> GenerateShift(string Service)
    {
        string cleanService = Service.Trim().ToUpper().Substring(0, 2);
        string username;
        var lastDate = await _context.DailyCounters.Where(dc => dc.ServiceName == cleanService).OrderByDescending(dc => dc.Day).FirstOrDefaultAsync();       
        if (lastDate == null)
        {
            var dailyCounter = new DailyCounter { ServiceName = cleanService, Day = DateTime.Today, Counter = 1};
            await _context.DailyCounters.AddAsync(dailyCounter);
            await _context.SaveChangesAsync();
            username = $"{1} - {cleanService}";
        }else{
            lastDate.Counter = lastDate.Counter+1;
            username = $"{lastDate.Counter} - {cleanService.Substring(0, 2)}";
            _context.DailyCounters.Update(lastDate);
            _context.SaveChanges();
        }
        return username;
    }

    public async Task ReassignShift(string Service)
    {                        
        var queues = _context.Queues.AsQueryable();
        queues = queues.Where(u => u.Status == "Por reasignar");
        var queueToReassign = queues.ToList();
        var SearchUsers = _context.Users.AsQueryable();
        foreach(var shift in queueToReassign){
            SearchUsers = SearchUsers.Where(u => u.Skills.ToLower().Contains("de donde frutas saco esto?") && u.Status == "LogIn");
            var availableUsers = SearchUsers.ToList();
            if (availableUsers.Any())
            {
                var userToAssign = availableUsers.FirstOrDefault();
                shift.UserId = userToAssign.Id;
                shift.Status = "En espera";
                _context.Queues.Update(shift);
                await _context.SaveChangesAsync();
            }
        }
    }
}
