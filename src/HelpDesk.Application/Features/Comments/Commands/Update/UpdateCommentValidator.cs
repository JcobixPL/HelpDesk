using FluentValidation;

namespace HelpDesk.Application.Features.Comments.Commands.Update;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Comment ID is required.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Comment content is required.")
            .MaximumLength(5000)
            .WithMessage("Comment cannot be longer than 5000 characters.");
    }
}