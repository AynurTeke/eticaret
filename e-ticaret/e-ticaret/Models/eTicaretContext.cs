using Microsoft.EntityFrameworkCore;

namespace e_ticaret.Models
{
    public class eTicaretContext:DbContext
    {
        public eTicaretContext(DbContextOptions<eTicaretContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-NQJARKP;Initial Catalog=ETicaret;User Id=sa;Password=1234");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<ItemStatus> ItemStatuses { get; set; }
        public DbSet<OrderDetailStatus> OrderDetailStatuses { get; set; }



    }
}
