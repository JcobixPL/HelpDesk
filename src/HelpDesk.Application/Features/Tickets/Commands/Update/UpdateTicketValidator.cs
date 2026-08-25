using FluentValidation;

namespace HelpDesk.Application.Features.Tickets.Commands.Update;

public class UpdateTicketValidator : AbstractValidator<UpdateTicketCommand>
{
    public UpdateTicketValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Ticket ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(1000)
            .WithMessage("Description cannot be longer than 1000 characters.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Priority must be a valid enum value.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Type must be a valid enum value.");
    }
}
