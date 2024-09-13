using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Bliss.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }

        public DbSet<Person> people { get; set; }

    }
}
