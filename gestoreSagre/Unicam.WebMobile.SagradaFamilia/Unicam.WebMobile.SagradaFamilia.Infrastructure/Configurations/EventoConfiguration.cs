using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Unicam.WebMobile.SagradaFamilia.Models.Entities;

namespace Unicam.WebMobile.SagradaFamilia.Infrastructure.Configurations
{
    public class EventoConfiguration : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(p => p.IdEvento);
            builder.Property(p => p.IdEvento).HasColumnName("IdEvento");
            builder.Property(p => p.InformazioniAggiuntive).HasColumnName("InformazioniAggiuntive");
            builder.HasOne(p => p.Sagra).WithMany(p => p.Eventi).HasForeignKey(p => p.IdEvento);
            builder.HasMany(p => p.StocksBiglietto).WithOne().HasForeignKey(p => p.IdStock);
        }
    }
}
