using Microsoft.EntityFrameworkCore;
using MonopolyStorage.DataAccess.Database.Configuration;
using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.DataAccess.Database.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<PalletEntity> Pallets { get; set; }
        public DbSet<BoxEntity> Boxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoxEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PalletEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
