using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IMediator _mediator;
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator = default)
        : base(options)
    {
        _mediator = mediator;
    }

    [Obsolete]
    public static readonly LoggerFactory ConsoleLoggerFactory
        = new LoggerFactory(new[] {
             new ConsoleLoggerProvider((category, level)
                 => category == DbLoggerCategory.Database.Command.Name
             && level == LogLevel.Information, true) });
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().OwnsOne(customer => customer.Address);
        modelBuilder.Entity<Location>().OwnsOne(location => location.Adddress);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = default;
        var domainEventEntities = ChangeTracker.Entries<BaseModel>()
            .Select(entityEntry => entityEntry.Entity)
            .Where(entity => entity.DomainEvents.Any())
            .ToArray();
        foreach (var entity in ChangeTracker.Entries<BaseModel>()
            .Where(e => (e.State == EntityState.Added || (e.State == EntityState.Modified)))
            .Select(x => x.Entity))
        {
            entity.Version++;
        }
        result = await base.SaveChangesAsync(cancellationToken);
        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
        return result;
    }
    public DbSet<Bin> Bins { get; private set; }
    public DbSet<Booking> Bookings { get; private set; }
    public DbSet<BookingDetail> BookingDetails { get; private set; }
    public DbSet<Brand> Brands { get; private set; }
    public DbSet<Card> Cards { get; private set; }
    public DbSet<CardLayout> CardLayouts { get; private set; }
    public DbSet<Contact> Contacts { get; private set; }
    public DbSet<ContactMessage> ContactMessages { get; private set; }
    public DbSet<Customer> Customers { get; private set; }
    public DbSet<CustomerLocation> CustomerLocations { get; private set; }
    public DbSet<Dashboard> Dashboards { get; private set; }
    public DbSet<DashboardCard> DashboardCards { get; private set; }
    public DbSet<DigitalAsset> DigitalAssets { get; private set; }
    public DbSet<Driver> Drivers { get; private set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<HtmlContent> HtmlContents { get; private set; }
    public DbSet<InventoryItem> InventoryItems { get; private set; }
    public DbSet<Location> Locations { get; private set; }
    public DbSet<SalesOrder> SalesOrders { get; private set; }
    public DbSet<SalesOrderDetail> SalesOrderDetails { get; private set; }
    public DbSet<Product> Products { get; private set; }
    public DbSet<ProductCategory> ProductCategories { get; private set; }
    public DbSet<ProfessionalServiceProvider> ProfessionalServiceProviders { get; private set; }
    public DbSet<Profile> Profiles { get; private set; }
    public DbSet<Role> Roles { get; private set; }
    public DbSet<Signature> Signatures { get; private set; }
    public DbSet<Survey> Surveys { get; private set; }
    public DbSet<Shipment> Shipments { get; private set; }
    public DbSet<ShipmentItem> ShipmentItems { get; private set; }
    public DbSet<ShipmentBooking> ShipmentBookings { get; private set; }
    public DbSet<ShipmentSalesOrder> ShipmentSalesOrders { get; private set; }
    public DbSet<Tax> Taxes { get; private set; }
    public DbSet<Tenant> Tenants { get; private set; }
    public DbSet<User> Users { get; private set; }
    public DbSet<Video> Videos { get; private set; }
    public DbSet<Warehouse> Warehouses { get; private set; }
}
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string>>() {
                 new KeyValuePair<string, string>("Data:DefaultConnection:ConnectionString", "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TheKidsToyHive;Integrated Security=SSPI;")
            })
            .Build();
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration["Data:DefaultConnection:ConnectionString"];
        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("KidsToyHive.Api"));
        return new AppDbContext(builder.Options);
    }
}
