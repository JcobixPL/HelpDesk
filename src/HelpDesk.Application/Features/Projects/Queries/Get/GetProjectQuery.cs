using HelpDesk.Application.DTOs.Projects;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Queries.Get;

public record GetProjectQuery : IRequest<IReadOnlyList<ProjectDto>>;
