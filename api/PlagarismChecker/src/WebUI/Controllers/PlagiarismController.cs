using Microsoft.AspNetCore.Mvc;
using PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism;
using PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarismForUser;
using System.Threading.Tasks;

namespace PlagarismChecker.WebUI.Controllers
{
    EnableCors("MyPolicy")]
    public class PlagiarismController : ApiController
    {
        [HttpPost]
        public async Task<CheckTextForPlagiarismDto> CheckTextForPlagiarism(CheckTextForPlagiarismCommand command)
        {
            var checkTextForPlagiarismDto = await Mediator.Send(command);
            return checkTextForPlagiarismDto;
        }

        [HttpPost("user")]
        public async Task<CheckTextForPlagiarismForUserDto> CheckTextForPlagiarismForUser(CheckTextForPlagiarismForUserCommand command)
        {
            var checkTextForPlagiarismDto = await Mediator.Send(command);
            return checkTextForPlagiarismDto;
        }
    }
}
