using HelpDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Context;

public class HelpDeskDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }

    public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options) : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("helpdesk");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(HelpDeskDbContext).Assembly);
    }
}
