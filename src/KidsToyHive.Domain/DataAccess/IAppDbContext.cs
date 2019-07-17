using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Brand> Brands { get; set; }
        DbSet<Card> Cards { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Dashboard> Dashboards { get; set; }
        DbSet<DashboardCard> DashboardCards { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<InventoryItem> InventoryItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
