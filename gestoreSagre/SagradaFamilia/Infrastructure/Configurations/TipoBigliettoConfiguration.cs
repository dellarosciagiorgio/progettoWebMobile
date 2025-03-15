using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class TipoBigliettoConfiguration : IEntityTypeConfiguration<TipoBiglietto>
    {
        public void Configure(EntityTypeBuilder<TipoBiglietto> builder)
        {
            builder.HasKey(p => p.IdTipo);
            builder.Property(p => p.IdTipo).HasColumnName("IdTipoBiglietto");
            builder.Property(p => p.Descrizione).HasColumnName("DescrizioneTipoBiglietto");

        }
    }
}
