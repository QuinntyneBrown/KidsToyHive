using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Domain.DataAccess
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardCard> DashboardCards { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
