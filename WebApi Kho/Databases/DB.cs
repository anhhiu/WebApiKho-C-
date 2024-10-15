using Microsoft.EntityFrameworkCore;
using WebApi_Kho.Models;

namespace WebApi_Kho.Databases
{
    public class DB : DbContext
    {
        public DB(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

    }
}
