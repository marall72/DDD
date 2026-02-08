using Customers.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Customers.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
