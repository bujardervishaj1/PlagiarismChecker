using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public AddUserCommandValidator(
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty!");


            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname must not be empty!");


            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username must not be empty!")
                .MustAsync(BeUniqueUsername).WithMessage("The specified username already exists.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty!");
        }

        public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _plagiarismCheckerDbContext.Users
                .AllAsync(l => l.Username != username);
        }
    }
}
