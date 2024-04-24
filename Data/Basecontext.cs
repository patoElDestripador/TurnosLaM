using Microsoft.EntityFrameworkCore;
using TurnosLaM.Models;

namespace TurnosLaM.Data
{

    public class BaseContext : DbContext
    {
        
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        { }

        public object UsersAdmin { get; internal set; }
    }

}

