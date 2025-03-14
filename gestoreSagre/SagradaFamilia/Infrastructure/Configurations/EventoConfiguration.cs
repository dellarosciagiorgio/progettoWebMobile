using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class EventoConfiguration : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(p => p.IdEvento);
            builder.Property(p => p.IdEvento).HasColumnName("IdEvento");
            builder.Property(p => p.InformazioniAggiuntive).HasColumnName("InformazioniAggiuntive");
            builder.HasOne(p => p.Sagra).WithMany(p => p.Eventi).HasForeignKey(p => p.IdEvento);
            builder.HasMany(p => p.StocksBiglietto).WithOne(p => p.Evento).HasForeignKey(p => p.IdStock);
        }
    }
}
