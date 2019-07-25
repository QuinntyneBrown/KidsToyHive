using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Booking> Bookings { get; }
        DbSet<BookingDetail> BookingDetails { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Card> Cards { get; }
        DbSet<CardLayout> CardLayouts { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Dashboard> Dashboards { get; }
        DbSet<DashboardCard> DashboardCards { get; }
        DbSet<DigitalAsset> DigitalAssets { get; }
        DbSet<HtmlContent> HtmlContents { get; }
        DbSet<InventoryItem> InventoryItems { get; }
        DbSet<SalesOrder> SalesOrders { get; }
        DbSet<SalesOrderDetail> SalesOrderDetails { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductCategory> ProductCategories { get; }
        DbSet<Profile> Profiles { get; }
        DbSet<Role> Roles { get; }
        DbSet<Shipment> Shipments { get; }
        DbSet<ShipmentBooking> ShipmentBookings { get; }
        DbSet<ShipmentSalesOrder> ShipmentSalesOrders { get; }
        DbSet<Signature> Signatures { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
