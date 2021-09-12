using FluentValidation;
using ImageGram.Core.Constant.Entity;

namespace ImageGram.Core.Application.Domain.Common.Account.Command.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(prop => prop.Name)
                .MaximumLength(CommonEntityConstant.NameLength)
                .NotEmpty();

            RuleFor(prop => prop.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(prop => prop.Password)
                .NotEmpty();
        }
    }
}