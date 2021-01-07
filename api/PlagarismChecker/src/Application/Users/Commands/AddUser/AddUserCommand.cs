using MediatR;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Helpers;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Commands.AddUser
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismChecerDbContext;

        public AddUserCommandHandler(
            IPlagiarismCheckerDbContext plagiarismChecerDbContext)
        {
            this._plagiarismChecerDbContext = plagiarismChecerDbContext;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _plagiarismChecerDbContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            if (userExist != null)
                return Guid.Empty;

            var salt = ExtensionMethods.GenerateSalt();

            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                Salt = salt,
                Password = request.Password.GenerateHashPassword(salt)
            };

            await _plagiarismChecerDbContext.Users.AddAsync(user);
            var saved = await _plagiarismChecerDbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
