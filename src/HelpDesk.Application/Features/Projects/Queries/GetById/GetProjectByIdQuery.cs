using HelpDesk.Application.DTOs.Projects;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Queries.GetById;

public record GetProjectByIdQuery(Guid Id) : IRequest<ProjectDto>;
