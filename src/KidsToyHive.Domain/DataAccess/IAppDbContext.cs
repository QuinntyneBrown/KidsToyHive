using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Bin> Bins { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<BookingDetail> BookingDetails { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Card> Cards { get; }
        DbSet<CardLayout> CardLayouts { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<ContactMessage> ContactMessages { get; }
        DbSet<Customer> Customers { get; }
        DbSet<CustomerLocation> CustomerLocations { get; }
        DbSet<Dashboard> Dashboards { get; }
        DbSet<DashboardCard> DashboardCards { get; }
        DbSet<DigitalAsset> DigitalAssets { get; }
        DbSet<Driver> Drivers { get; }
        DbSet<EmailTemplate> EmailTemplates { get; }
        DbSet<HtmlContent> HtmlContents { get; }
        DbSet<InventoryItem> InventoryItems { get; }
        DbSet<Location> Locations { get; }
        DbSet<SalesOrder> SalesOrders { get; }
        DbSet<SalesOrderDetail> SalesOrderDetails { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductCategory> ProductCategories { get; }
        DbSet<Profile> Profiles { get; }
        DbSet<Role> Roles { get; }
        DbSet<Shipment> Shipments { get; }
        DbSet<ShipmentItem> ShipmentItems { get; }
        DbSet<ShipmentBooking> ShipmentBookings { get; }
        DbSet<ShipmentSalesOrder> ShipmentSalesOrders { get; }
        DbSet<Signature> Signatures { get; }
        DbSet<Tax> Taxes { get; }
        DbSet<User> Users { get; }
        DbSet<Video> Videos { get; }
        DbSet<Warehouse> Warehouses { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
