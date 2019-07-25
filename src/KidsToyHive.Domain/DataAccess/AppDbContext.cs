using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

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

        public DbSet<Booking> Bookings { get; private set; }
        public DbSet<BookingDetail> BookingDetails { get; private set; }
        public DbSet<Brand> Brands { get; private set; }
        public DbSet<Card> Cards { get; private set; }
        public DbSet<CardLayout> CardLayouts { get; private set; }
        public DbSet<Customer> Customers { get; private set; }
        public DbSet<CustomerLocation> CustomerLocations { get; private set; }
        public DbSet<Dashboard> Dashboards { get; private set; }
        public DbSet<DashboardCard> DashboardCards { get; private set; }
        public DbSet<DigitalAsset> DigitalAssets { get; private set; }
        public DbSet<HtmlContent> HtmlContents { get; private set; }
        public DbSet<InventoryItem> InventoryItems { get; private set; }
        public DbSet<Location> Locations { get; private set; }
        public DbSet<SalesOrder> SalesOrders { get; private set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<ProductCategory> ProductCategories { get; private set; }
        public DbSet<Profile> Profiles { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<Shipment> Shipments { get; private set; }
        public DbSet<ShipmentBooking> ShipmentBookings { get; private set; }
        public DbSet<ShipmentSalesOrder> ShipmentSalesOrders { get; private set; }
        public DbSet<User> Users { get; private set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string,string>>() {
                    new KeyValuePair<string, string>("Data:DefaultConnection:ConnectionString", "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TheKidsToyHive;Integrated Security=SSPI;")
                })
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = configuration["Data:DefaultConnection:ConnectionString"];

            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("KidsToyHive.Api"));

            return new AppDbContext(builder.Options);
        }
    }
}
