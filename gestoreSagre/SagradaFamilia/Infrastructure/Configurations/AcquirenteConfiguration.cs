using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class AcquirenteConfiguration : IEntityTypeConfiguration<Acquirente>
    {
        public void Configure(EntityTypeBuilder<Acquirente> builder)
        {
            builder.HasKey(p => p.IdAcquirente);
            builder.Property(p => p.IdAcquirente).HasColumnName("IdAcquirente");
            builder.Property(p => p.Nome).HasColumnName("NomeAcquirente");
            builder.Property(p => p.Cognome).HasColumnName("CognomeAcquirente");
            builder.Property(p => p.Mail).HasColumnName("MailAcquirente");
            builder.HasMany(x => x.BigliettiComprati).WithOne(x => x.Acquirente).HasForeignKey(x => x.IdBiglietto);
        }
    }
}
