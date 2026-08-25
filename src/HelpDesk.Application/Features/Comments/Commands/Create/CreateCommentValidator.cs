using FluentValidation;

namespace HelpDesk.Application.Features.Comments.Commands.Create;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Comment content is required.")
            .MaximumLength(5000)
            .WithMessage("Comment cannot be longer than 5000 characters.");

        RuleFor(x => x.TicketId)
            .NotEmpty()
            .WithMessage("Ticket ID is required.");
    }
}
