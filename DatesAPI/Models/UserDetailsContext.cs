using Microsoft.EntityFrameworkCore;

namespace DatesAPI.Models
{
    public class UserDetailsContext : DbContext
    {
        public UserDetailsContext(DbContextOptions<UserDetailsContext> options) : base(options)
        {
            
        }

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
