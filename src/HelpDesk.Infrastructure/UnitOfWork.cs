using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Infrastructure.Context;

namespace HelpDesk.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly HelpDeskDbContext _context;

    public UnitOfWork(HelpDeskDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
