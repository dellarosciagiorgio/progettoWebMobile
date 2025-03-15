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
            builder.Property(p => p.IdBiglietto).HasColumnName("IdBiglietto");
            builder.Property(p => p.Nominativo).HasColumnName("NominativoBiglietto");
            builder.HasOne(x => x.TipoBiglietto)
                .WithMany()
                .HasForeignKey(x => x.IdTipoBiglietto);

        }
    }
}
