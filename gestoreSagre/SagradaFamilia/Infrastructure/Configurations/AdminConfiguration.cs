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
    internal class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(p => p.IdAdmin);
            builder.Property(p => p.IdAdmin).HasColumnName("IdAdmin");
            builder.Property(p => p.Nome).HasColumnName("NomeAdmin");
            builder.Property(p => p.Cognome).HasColumnName("CognomeAdmin");
            builder.Property(p => p.Mail).HasColumnName("MailAdmin");

        }
    }
}
