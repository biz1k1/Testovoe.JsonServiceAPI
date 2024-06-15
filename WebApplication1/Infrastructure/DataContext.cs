using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entity;

namespace WebApplication1.Infrastructure
{
    public class DataContext:DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }

        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());

        }
    }
}
