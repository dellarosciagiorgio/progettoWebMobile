using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Models.DetailedEntities;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.IdUser);
            builder.Property(p => p.IdUser).HasColumnName("IdUser");
            builder.Property(p => p.Email).HasColumnName("Mail");
            builder.Property(p => p.Password).HasColumnName("Pass");
            builder.Property(p => p.Ruolo).HasColumnName("Ruolo");
            // Mappa il campo stringa "Ruolo" al tipo enum "Ruolo"
            builder
                .Property(u => u.Ruolo)
                .HasConversion(
                    v => v.ToString(), // Converte Enum in stringa per il database
                    v => (Ruolo)Enum.Parse(typeof(Ruolo), v)); // Converte stringa in Enum quando carica i dati
        }
    }
}
