using AutoMapper;
using MediatR;
using PlagarismChecker.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly IMapper _mapper;
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public GetUserQueryHandler(
            IMapper mapper,
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._mapper = mapper;
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _plagiarismCheckerDbContext.Users.FindAsync(request.Id);
            if (user == null)
                return null;

            return _mapper.Map<GetUserDto>(user);
        }
       
    }
}
