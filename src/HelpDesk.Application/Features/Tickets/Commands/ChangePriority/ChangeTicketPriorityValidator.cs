using FluentValidation;

namespace HelpDesk.Application.Features.Tickets.Commands.ChangePriority;

public class ChangeTicketPriorityValidator : AbstractValidator<ChangeTicketPriorityCommand>
{
    public ChangeTicketPriorityValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty()
            .WithMessage("Ticket ID is required.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Priority must be a valid enum value.");
    }
}