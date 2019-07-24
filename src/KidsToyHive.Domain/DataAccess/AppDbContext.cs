using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Domain.DataAccess
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Brand> Brands { get; private set; }
        public DbSet<Card> Cards { get; private set; }
        public DbSet<CardLayout> CardLayouts { get; private set; }
        public DbSet<Customer> Customers { get; private set; }
        public DbSet<Dashboard> Dashboards { get; private set; }
        public DbSet<DashboardCard> DashboardCards { get; private set; }
        public DbSet<DigitalAsset> DigitalAssets { get; private set; }
        public DbSet<HtmlContent> HtmlContents { get; private set; }
        public DbSet<InventoryItem> InventoryItems { get; private set; }
        public DbSet<Order> Orders { get; private set; }
        public DbSet<OrderItem> OrderItems { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<ProductCategory> ProductCategories { get; private set; }
        public DbSet<Profile> Profiles { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<User> Users { get; private set; }
    }
}
