using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Unicam.WebMobile.SagradaFamilia.Models.Entities;

namespace Unicam.WebMobile.SagradaFamilia.Infrastructure.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(p => p.IdFeedback);
            builder.Property(p => p.IdFeedback).HasColumnName("IdFeedback");
            builder.Property(p => p.Acquirente).HasColumnName("Acquirente");
            builder.Property(p => p.Descrizione).HasColumnName("Descrizione");
            builder.HasOne(p => p.Acquirente).WithMany(p => p.FeedBacks).HasForeignKey(p => p.IdFeedback);

        }
    }
}
