using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarismForUser
{
    public class CheckTextForPlagiarismForUserCommandValidator : AbstractValidator<CheckTextForPlagiarismForUserCommand>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public CheckTextForPlagiarismForUserCommandValidator(
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username must not be empty!")
                .MustAsync(UserNotExists).WithMessage("The specified username does not exists!");

            RuleFor(x => x.TextToCheck)
                .NotEmpty().WithMessage("TextToCheck must not be empty!");
        }

        public async Task<bool> UserNotExists(string username, CancellationToken cancellationToken)
        {
            return await _plagiarismCheckerDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower()) != null;
        }
    }
}
