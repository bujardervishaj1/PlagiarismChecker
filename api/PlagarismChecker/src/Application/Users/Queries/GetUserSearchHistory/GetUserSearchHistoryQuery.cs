using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Users.Queries.GetUserSearchHistory
{
    public class GetUserSearchHistoryQuery : IRequest<GetUserSearchHistoryDto>
    {
        public string Username { get; set; }
    }

    public class GetUserSearchHistoryQueryHandler : IRequestHandler<GetUserSearchHistoryQuery, GetUserSearchHistoryDto>
    {
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;
        private readonly IMapper _mapper;

        public GetUserSearchHistoryQueryHandler(
            IPlagiarismCheckerDbContext plagiarismCheckerDbContext,
            IMapper mapper
            )
        {
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;
            this._mapper = mapper;
        }

        public async Task<GetUserSearchHistoryDto> Handle(GetUserSearchHistoryQuery request, CancellationToken cancellationToken)
        {
            var histories = await _plagiarismCheckerDbContext.UserHistory.Where(x => x.Username.ToLower() == request.Username.ToLower()).ToListAsync();

            var userSearchHistoryDto = _mapper.Map<GetUserSearchHistoryDto>(histories);

            return userSearchHistoryDto;
        }
    }
}
