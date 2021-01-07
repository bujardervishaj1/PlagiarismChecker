using MediatR;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Helpers;
using PlagarismChecker.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<bool>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public LoginUserCommandHandler(
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _plagiarismCheckerDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());

            return request.Password.VerifyHash(user.Salt, user.Password);
        }
    }
}
