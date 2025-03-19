using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class SagraConfiguration : IEntityTypeConfiguration<Sagra>
    {
        public void Configure(EntityTypeBuilder<Sagra> builder)
        {
            builder.HasKey(p => p.IdSagra);
            builder.Property(p => p.IdSagra).HasColumnName("IdSagra");
            builder.Property(p => p.Descrizione).HasColumnName("DescrizioneSagra");
            builder.HasOne(p => p.Organizzatore).WithMany(p => p.Sagre).HasForeignKey(x => x.IdOrganizzatore);

        }
    }
}
