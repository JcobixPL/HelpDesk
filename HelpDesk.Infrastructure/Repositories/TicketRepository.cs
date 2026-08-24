using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly HelpDeskDbContext _context;

    public TicketRepository(HelpDeskDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public void Add(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
    }

    public void Update(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
    }
} 
