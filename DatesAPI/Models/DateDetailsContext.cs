using Microsoft.EntityFrameworkCore;

namespace DatesAPI.Models
{
    public class DateDetailsContext : DbContext
    {
        public DateDetailsContext(DbContextOptions<DateDetailsContext> options) : base(options)
        {
            
        }

        public DbSet<DateDetails> DateDetails { get; set; }
    }
}
