using FluentValidation;

namespace PlagarismChecker.Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
    {
        public GetUserByUsernameQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username must not be empty!");
        }
    }
}
