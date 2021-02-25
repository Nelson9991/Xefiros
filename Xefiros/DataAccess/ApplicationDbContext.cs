using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xefiros.Shared.Models;

namespace Xefiros.DataAccess
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>()
                .HasIndex(x => x.Cedula).IsUnique(true);

            builder.Entity<Producto>()
                .HasIndex(x => x.Codigo).IsUnique(true);

            builder.Entity<Venta>()
                .HasOne(x => x.Cliente)
                .WithMany(x => x.Ventas)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DetalleVenta>()
                .HasOne(x => x.Producto)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Abono> Abonos { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
