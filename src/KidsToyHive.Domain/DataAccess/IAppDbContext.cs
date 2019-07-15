using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Brand> Brands { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
