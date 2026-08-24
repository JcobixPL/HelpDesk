using FluentValidation;

namespace HelpDesk.Application.Features.Users.Commands.Deactivate;

public class DeactivateUserValidator : AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
}