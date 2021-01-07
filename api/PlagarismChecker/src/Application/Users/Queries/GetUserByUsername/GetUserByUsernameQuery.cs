using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQuery : IRequest<GetUserByUsernameDto>
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, GetUserByUsernameDto>
    {
        private readonly IMapper _mapper;
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public GetUserByUsernameQueryHandler(
            IMapper mapper,
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._mapper = mapper;
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;
        }

        public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _plagiarismCheckerDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());
            if (user == null)
                return null;

            return _mapper.Map<GetUserByUsernameDto>(user);
        }
    }
}
