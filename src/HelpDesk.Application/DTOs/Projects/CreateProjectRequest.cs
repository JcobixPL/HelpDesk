namespace HelpDesk.Application.DTOs.Projects;

public record CreateProjectRequest(
    string Name,
    string Key,
    string Description,
    Guid CreatedById);