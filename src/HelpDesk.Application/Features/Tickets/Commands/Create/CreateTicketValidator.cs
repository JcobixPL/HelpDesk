using FluentValidation;

namespace HelpDesk.Application.Features.Tickets.Commands.Create;

public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot be longer than 1000 characters.");

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.");

        RuleFor(x => x.ReporterId)
            .NotEmpty().WithMessage("ReporterId is required.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Priority must be a valid enum value.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Type must be a valid enum value.");
    }
}
