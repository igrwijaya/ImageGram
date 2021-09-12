using FluentValidation;

namespace ImageGram.Core.Application.Domain.Common.Account.Queries.AccountLogin
{
    public class AccountLoginQueryValidator : AbstractValidator<AccountLoginQuery>
    {
        public AccountLoginQueryValidator()
        {
            RuleFor(prop => prop.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(prop => prop.Password)
                .NotEmpty();
        }
    }
}