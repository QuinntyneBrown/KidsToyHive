using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<Conversation> Conversations { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactRequest> ContactRequests { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
