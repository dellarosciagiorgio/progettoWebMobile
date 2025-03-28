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
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("IdAdmin");
            builder.Property(p => p.Nome).HasColumnName("NomeAdmin");
            builder.Property(p => p.Cognome).HasColumnName("CognomeAdmin"); 
            builder.HasOne(p => p.User).WithOne().HasForeignKey<Admin>(p => p.IdUser);
        }
    }
}
