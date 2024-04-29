using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TurnosLaM.Data;
using TurnosLaM.Models;


namespace TurnosLaM.Helpers
{
    public class DailyCounterHelper
    {
        public readonly BaseContext _context; 
        public DailyCounterHelper(BaseContext context)
        {
            _context = context;
        }
        public void CreateNewDailyCounter()
        {
            var lastDate = _context.DailyCounters.OrderByDescending(dc => dc.Day).FirstOrDefault();
            var today = DateTime.Today;
            var lastDay = lastDate?.Day.Date;
            if (lastDay!= null && lastDay < today)
            {
                var lastDateTime = lastDate?.Day;
                if (lastDateTime!= null && lastDateTime.Value.Hour == 0 && lastDateTime.Value.Minute == 0)
                {
                    var newDailyCounter = new DailyCounter
                    {
                        Day = today,
                        Counter = 0
                    };
                    _context.DailyCounters.Add(newDailyCounter);
                    _context.SaveChanges();
                }
            }
        }
    }
}