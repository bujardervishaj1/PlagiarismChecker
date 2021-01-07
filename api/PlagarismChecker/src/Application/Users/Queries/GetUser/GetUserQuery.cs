using MediatR;
using PlagarismChecker.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using System.Text;
using PlagarismChecker.Domain.Entities;

namespace PlagarismChecker.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserDto>
    {
        public string Username { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismChecerDbContext;

        public GetUserQueryHandler(
            IPlagiarismCheckerDbContext plagiarismChecerDbContext)
        {
            this._plagiarismChecerDbContext = plagiarismChecerDbContext;
        }

        public Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {

            var password = "123456";

            var t = GenerateSalt();
            var tt = GenerateHashPassword(password, t);

            var verify = VerifyHash(password, t, tt);

            var user1 = new User
            {
                Name = "Bujar",
                Surname = "Dervishaj",
                Username = "bujardervishaj",
                Password = tt,
                Salt = t
            };

            _plagiarismChecerDbContext.Users.Add(user1);
            _plagiarismChecerDbContext.SaveChangesAsync(cancellationToken);

            return null;
        }

        private byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[16];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }

        private byte[] GenerateHashPassword(string password, byte[] salt) =>
             new Argon2id(Encoding.UTF8.GetBytes(password))
             {
                 Salt = salt,
                 Iterations = 2,
                 MemorySize = 1024 * 4,
                 DegreeOfParallelism = 2
             }.GetBytes(16);

        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = GenerateHashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }
}
