using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class OrganizzatoreConfiguration : IEntityTypeConfiguration<Organizzatore>
    {
        public void Configure(EntityTypeBuilder<Organizzatore> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("IdOrganizzatore");
            builder.Property(p => p.NomeOrganizzazione).HasColumnName("NomeOrganizzatore");
            builder.HasOne(p => p.User).WithOne().HasForeignKey<Organizzatore>(p => p.IdUser);
        }
    }
}
