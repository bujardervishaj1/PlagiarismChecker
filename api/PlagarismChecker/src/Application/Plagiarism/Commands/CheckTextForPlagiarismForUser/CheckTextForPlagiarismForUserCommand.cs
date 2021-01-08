using AutoMapper;
using MediatR;
using PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism;
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

        public CheckTextForPlagiarismForUserCommandHandler(
             IRequestHandler<CheckTextForPlagiarismCommand, CheckTextForPlagiarismDto> checkTextForPlagiarism,
             IMapper mapper
            )
        {
            this._checkTextForPlagiarism = checkTextForPlagiarism;
            this._mapper = mapper;
        }

        public async Task<CheckTextForPlagiarismForUserDto> Handle(CheckTextForPlagiarismForUserCommand request, CancellationToken cancellationToken)
        {
            var checkTextForPlagiarismDto = await _checkTextForPlagiarism.Handle(new CheckTextForPlagiarismCommand { TextToCheck = request.TextToCheck }, cancellationToken);

            var checkTextForPlagiarismForUserDto = _mapper.Map<CheckTextForPlagiarismForUserDto>(checkTextForPlagiarismDto);
            checkTextForPlagiarismForUserDto.Username = request.Username;

            return checkTextForPlagiarismForUserDto;
        }
    }
}
