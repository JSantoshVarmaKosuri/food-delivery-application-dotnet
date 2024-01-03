using fdorder.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace fdorder
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=orders;user=root;password=qwert;AllowPublicKeyRetrieval=true");
        }
    }
}