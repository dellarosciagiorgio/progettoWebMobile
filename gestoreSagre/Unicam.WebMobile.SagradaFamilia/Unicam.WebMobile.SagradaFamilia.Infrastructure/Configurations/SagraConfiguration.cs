using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Unicam.WebMobile.SagradaFamilia.Models.Entities;

namespace Unicam.WebMobile.SagradaFamilia.Infrastructure.Configurations
{
    public class SagraConfiguration : IEntityTypeConfiguration<Sagra>
    {
        public void Configure(EntityTypeBuilder<Sagra> builder)
        {
            builder.HasKey(p => p.IdSagra);
            builder.Property(p => p.IdSagra).HasColumnName("IdSagra");
            builder.Property(p => p.Descrizione).HasColumnName("DescrizioneSagra");
            builder.HasMany(x => x.Eventi).WithOne().HasForeignKey(x => x.IdEvento);

        }
    }
}
