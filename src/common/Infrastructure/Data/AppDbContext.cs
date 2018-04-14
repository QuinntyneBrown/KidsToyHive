using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Content> Contents { get; set; }
        DbSet<ContactRequest> ContactRequests { get; set; }
        DbSet<Dashboard> Dashboards { get; set; }
        DbSet<DashboardCard> DashboardCards { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardCard> DashboardCards { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
