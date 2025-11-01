using Microsoft.EntityFrameworkCore;
using technova_ecommerce.Models.Entities;

namespace technova_ecommerce.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
