using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Abstractions.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(Project project);
}
