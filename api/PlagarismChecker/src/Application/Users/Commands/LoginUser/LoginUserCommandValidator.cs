using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public LoginUserCommandValidator(
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username must not be empty!")
                .MustAsync(BeUniqueUsername).WithMessage("The specified username does not exists!");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty!");
        }

        public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _plagiarismCheckerDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower()) != null;
        }
    }
}
