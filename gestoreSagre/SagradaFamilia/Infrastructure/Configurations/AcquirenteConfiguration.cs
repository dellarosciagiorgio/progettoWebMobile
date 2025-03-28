using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class AcquirenteConfiguration : IEntityTypeConfiguration<Acquirente>
    {
        public void Configure(EntityTypeBuilder<Acquirente> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("IdAcquirente");
            builder.Property(p => p.Nome).HasColumnName("NomeAcquirente");
            builder.Property(p => p.Cognome).HasColumnName("CognomeAcquirente");
            builder.HasOne(p => p.User).WithOne().HasForeignKey<Acquirente>(p => p.IdUser);
        }
    }
}
