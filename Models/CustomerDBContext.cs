using Microsoft.EntityFrameworkCore;
namespace customer_data_webAPI.Models
{
    public class CustomerDBContext : DbContext
    {

        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {

        }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<User>? Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                    .HasOne(a => a.Address)
                    .WithOne(b => b.Customer_address)
                    .HasForeignKey<Address>(b => b.Cus_address);
        }
    }
}