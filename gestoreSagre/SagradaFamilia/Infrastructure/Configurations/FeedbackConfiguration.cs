using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(p => p.IdFeedback);
            builder.Property(p => p.IdFeedback).HasColumnName("IdFeedback");
            builder.Property(p => p.Descrizione).HasColumnName("Descrizione");
            //builder.HasOne(p => p.Acquirente).WithMany(p => p.Feedbacks).HasForeignKey(p => p.IdAcquirente);

        }
    }
}
