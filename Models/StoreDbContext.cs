using System.Security.Cryptography.X509Certificates;

namespace SportsStore.Models
{
    public class StoreDbContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }   
        public DbSet<Product> Products { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) 
        {
            
        }
    }
}
