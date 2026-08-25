using HelpDesk.Application.DTOs.Projects;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Commands.Update;

public record UpdateProjectCommand(
    Guid Id,
    string Name,
    string Description) : IRequest<ProjectDto>;
