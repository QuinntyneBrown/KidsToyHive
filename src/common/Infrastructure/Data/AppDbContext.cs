using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Brand> Brands { get; set; }
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
        DbSet<Tenant> Tenants { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class AppDbContext: DbContext, IAppDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : this(options)
        {
            _httpContextAccessor = httpContextAccessor;             
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Brand> Brands { get; set; }
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
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => SaveChangesAsync(cancellationToken, new Guid($"{_httpContextAccessor.HttpContext.Items["TenantId"]}"), $"{_httpContextAccessor.HttpContext.Items["Username"]}");

        public void UpdateLoggableEntities(string username) {
            foreach (var entity in ChangeTracker.Entries()
                .Where(e => e.Entity is ILoggable && ((e.State == EntityState.Added || (e.State == EntityState.Modified))))
                .Select(x => x.Entity as ILoggable))
            {
                var isNew = entity.CreatedOn == default(DateTime);
                entity.CreatedOn = isNew ? DateTime.UtcNow : entity.CreatedOn;
                entity.CreatedBy = isNew ? username : entity.CreatedBy;
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.LastModifiedBy = username;
            }
        }

        public void SetTenantIdOnAddedEntities(Guid tenantId) {
            foreach (var item in ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added && e.Metadata.GetProperties().Any(p => p.Name == "TenantId")))
            {
                item.CurrentValues["TenantId"] = tenantId;
            }
        }


        public void SoftDeleteEntities()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }
        }

        public void PreSaveChangesProcessing(string username, Guid tenantId) {

            UpdateLoggableEntities(username);

            SetTenantIdOnAddedEntities(tenantId);

            SoftDeleteEntities();
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken, Guid tenantId, string username)
        {
            ChangeTracker.DetectChanges();

            PreSaveChangesProcessing(username, tenantId);

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var tenantId = new Guid($"{_httpContextAccessor.HttpContext.Items["TenantId"]}");
            var username = $"{_httpContextAccessor.HttpContext.Items["Username"]}";

            PreSaveChangesProcessing(username, tenantId);

            return base.SaveChanges();
        }

        private static IList<Type> _entityTypeCache = default(IList<Type>);
        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != default(IList<Type>))
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseEntity)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(AppDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(mb =>
            {
                mb.Property(t => t.TenantId).HasDefaultValueSql("newsequentialid()");
            });

        }



        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>()
                .HasQueryFilter(e => EF.Property<Guid>(e, "TenantId") == new Guid($"{_httpContextAccessor.HttpContext.Items["TenantId"]}") && !e.IsDeleted);
        }
        
    }    
}
