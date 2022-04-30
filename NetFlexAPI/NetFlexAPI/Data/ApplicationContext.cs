using Microsoft.EntityFrameworkCore;
using NetFlexAPI.Models;

namespace NetFlexAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=username; Server=localhost; port=54321; Database=netflex; Password=password; Pooling=true;");
        }
    }
}