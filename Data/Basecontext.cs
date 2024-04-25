using Microsoft.EntityFrameworkCore;
using TurnosLaM.Models;

namespace TurnosLaM.Data
{

    public class BaseContext : DbContext
    {
        
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        { 

        }
        public object UsersAdmin { get; internal set; }
        // SE REGISTRAN LOS MODELOS EN Context:
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<QueueStatus> QueueStatus { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ShiftModel> Shifts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DailyCounter> DailyCounters { get; set; }
        public DbSet<QueueToReassign> ViewQueueToReassign { get; set; }

    }

}
