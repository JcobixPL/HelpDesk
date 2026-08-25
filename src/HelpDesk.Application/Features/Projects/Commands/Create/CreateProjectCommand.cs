using HelpDesk.Application.DTOs.Projects;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Commands.Create;

public record CreateProjectCommand(
    string Name,
    string Key, 
    string Description,
    Guid CreatedById) : IRequest<ProjectDto>;
