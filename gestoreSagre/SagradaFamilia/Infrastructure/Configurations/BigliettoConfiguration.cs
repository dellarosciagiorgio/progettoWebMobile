using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Infrastructure.Configurations
{
    internal class BigliettoConfiguration : IEntityTypeConfiguration<Biglietto>
    {
        public void Configure(EntityTypeBuilder<Biglietto> builder)
        {
            builder.HasKey(p => p.IdBiglietto);
            
            builder.Property(b => b.IdBiglietto).HasColumnName("IdBiglietto");
            builder.Property(p => p.Nominativo).HasColumnName("Nominativo");
            builder.HasOne(x => x.TipoBiglietto)
                .WithMany()
                .HasForeignKey(x => x.IdTipoBiglietto)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Acquirente)
                .WithMany(x => x.BigliettiComprati)
                .HasForeignKey(x => x.IdAcquirente)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
