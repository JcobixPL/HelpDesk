using FluentValidation;

namespace HelpDesk.Application.Features.Projects.Commands.Create;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required.")
            .MaximumLength(200)
            .WithMessage("Project name cannot be longer than 200 characters.");

        RuleFor(x => x.Key)
            .NotEmpty()
            .WithMessage("Project key is required.")
            .MaximumLength(10)
            .WithMessage("Project key cannot be longer than 10 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(256)
            .WithMessage("Description cannot be longer than 256 characters.");

        RuleFor(x => x.CreatedById)
            .NotEmpty()
            .WithMessage("Created by user ID is required.");
    }
}
