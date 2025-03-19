using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class OrganizzatoreConfiguration : IEntityTypeConfiguration<Organizzatore>
    {
        public void Configure(EntityTypeBuilder<Organizzatore> builder)
        {
            builder.HasKey(p => p.IdOrganizzatore);
            builder.Property(p => p.IdOrganizzatore).HasColumnName("IdOrganizzatore");
            builder.Property(p => p.Mail).HasColumnName("MailOrganizzatore");
        }
    }
}
