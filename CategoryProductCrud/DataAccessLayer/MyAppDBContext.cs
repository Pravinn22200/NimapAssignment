using CategoryProductCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryProductCrud.DataAccessLayer
{
    public class MyAppDBContext : DbContext
    {
        public MyAppDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
