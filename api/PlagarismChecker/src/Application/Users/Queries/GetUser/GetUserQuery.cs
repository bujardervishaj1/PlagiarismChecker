using MediatR;
using PlagarismChecker.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var t = _plagiarismChecerDbContext.Users.Where(x => x.Username.Equals(request.Username));

            return null;
        }
    }
}
