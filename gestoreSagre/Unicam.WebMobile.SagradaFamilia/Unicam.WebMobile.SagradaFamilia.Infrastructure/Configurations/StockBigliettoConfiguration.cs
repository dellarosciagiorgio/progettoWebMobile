using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Unicam.WebMobile.SagradaFamilia.Models.Entities;

namespace Unicam.WebMobile.SagradaFamilia.Infrastructure.Configurations
{
    public class StockBigliettoConfiguration : IEntityTypeConfiguration<StockBiglietto>
    {
        public void Configure(EntityTypeBuilder<StockBiglietto> builder)
        {
            builder.HasKey(p => p.IdStock);
            builder.Property(p => p.IdStock).HasColumnName("IdStock");
            builder.Property(p => p.Quantita).HasColumnName("Quantita");

            builder.HasOne(x => x.TipoBiglietto).WithOne(x => x.StockBiglietto)
                .HasForeignKey<StockBiglietto>(x => x.IdStock);

        }
    }
}
