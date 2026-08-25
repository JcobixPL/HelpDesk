using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly HelpDeskDbContext _context;

    public ProjectRepository(HelpDeskDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Add(Project project)
    {
        _context.Projects.Add(project);
    }

    public void Update(Project project)
    {
        _context.Projects.Update(project);
    }
}
