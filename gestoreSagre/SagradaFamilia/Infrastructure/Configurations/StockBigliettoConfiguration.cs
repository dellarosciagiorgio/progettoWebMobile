using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class StockBigliettoConfiguration : IEntityTypeConfiguration<StockBiglietto>
    {
        public void Configure(EntityTypeBuilder<StockBiglietto> builder)
        {
            builder.HasKey(p => p.IdStock);
            builder.Property(p => p.IdStock).HasColumnName("IdStock");
            builder.Property(p => p.Quantita).HasColumnName("Quantita");

            builder.HasOne(x => x.TipoBiglietto)
                .WithOne()
                .HasForeignKey<StockBiglietto>(x => x.IdTipoBiglietto);

            builder.HasOne(x => x.Evento).WithMany(x => x.Stocks).HasForeignKey(x => x.IdEvento);

        }
    }
}
