using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism;
using PlagarismChecker.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarismForUser
{
    public class CheckTextForPlagiarismForUserCommand : IRequest<CheckTextForPlagiarismForUserDto>
    {
        public string Username { get; set; }

        public string TextToCheck { get; set; }
    }

    public class CheckTextForPlagiarismForUserCommandHandler : IRequestHandler<CheckTextForPlagiarismForUserCommand, CheckTextForPlagiarismForUserDto>
    {
        private readonly IRequestHandler<CheckTextForPlagiarismCommand, CheckTextForPlagiarismDto> _checkTextForPlagiarism;
        private readonly IMapper _mapper;
        private readonly IPlagiarismCheckerDbContext _plagiarismCheckerDbContext;

        public CheckTextForPlagiarismForUserCommandHandler(
             IRequestHandler<CheckTextForPlagiarismCommand, CheckTextForPlagiarismDto> checkTextForPlagiarism,
             IMapper mapper,
             IPlagiarismCheckerDbContext plagiarismCheckerDbContext
            )
        {
            this._checkTextForPlagiarism = checkTextForPlagiarism;
            this._mapper = mapper;
            this._plagiarismCheckerDbContext = plagiarismCheckerDbContext;
        }

        public async Task<CheckTextForPlagiarismForUserDto> Handle(CheckTextForPlagiarismForUserCommand request, CancellationToken cancellationToken)
        {
            var checkTextForPlagiarismDto = await _checkTextForPlagiarism.Handle(new CheckTextForPlagiarismCommand { TextToCheck = request.TextToCheck }, cancellationToken);

            var checkTextForPlagiarismForUserDto = _mapper.Map<CheckTextForPlagiarismForUserDto>(checkTextForPlagiarismDto);
            checkTextForPlagiarismForUserDto.Username = request.Username;

            SaveToDb(request, checkTextForPlagiarismForUserDto, cancellationToken);

            return checkTextForPlagiarismForUserDto;
        }

        private async void SaveToDb(CheckTextForPlagiarismForUserCommand request, CheckTextForPlagiarismForUserDto checkTextForPlagiarismForUserDto, CancellationToken cancellationToken)
        {
            var checkTextForPlagiarismForUserDtoToString = JsonConvert.SerializeObject(checkTextForPlagiarismForUserDto);

            var userHistory = new UserHistory
            {
                Result = checkTextForPlagiarismForUserDtoToString,
                Text = request.TextToCheck,
                Username = request.Username
            };

            await _plagiarismCheckerDbContext.UserHistory.AddAsync(userHistory);
            await _plagiarismCheckerDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
