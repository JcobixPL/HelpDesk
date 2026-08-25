using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Repositories;

public class TicketHistoryRepository : ITicketHistoryRepository
{
    private readonly HelpDeskDbContext _context;

    public TicketHistoryRepository(HelpDeskDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TicketHistory>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        return await _context.TicketHistories
            .AsNoTracking()
            .Where(x => x.TicketId == ticketId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Add(TicketHistory history)
    {
        _context.TicketHistories.Add(history);
    }
}
