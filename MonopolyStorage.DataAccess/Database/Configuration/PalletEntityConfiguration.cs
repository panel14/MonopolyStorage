using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.DataAccess.Database.Configuration
{
    public class PalletEntityConfiguration : IEntityTypeConfiguration<PalletEntity>
    {
        public void Configure(EntityTypeBuilder<PalletEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.ToTable(t => t.HasCheckConstraint("ValidWidth", "\"Width\" > 0"));
            builder.ToTable(t => t.HasCheckConstraint("ValidHeight", "\"Height\" > 0"));
            builder.ToTable(t => t.HasCheckConstraint("ValidDepth", "\"Depth\" > 0"));
        }
    }
}
