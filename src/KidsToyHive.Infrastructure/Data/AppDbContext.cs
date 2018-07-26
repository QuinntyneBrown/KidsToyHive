using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KidsToyHive.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options) { }

        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}
