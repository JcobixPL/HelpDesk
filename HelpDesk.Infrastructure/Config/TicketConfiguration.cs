using HelpDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Infrastructure.Config;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Key)
            .IsUnique();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.ResolvedAt)
            .IsRequired(false);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Reporter)
            .WithMany()
            .HasForeignKey(x => x.ReporterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Assignee)
            .WithMany()
            .HasForeignKey(x => x.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Ticket)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
