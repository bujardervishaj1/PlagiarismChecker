using Microsoft.AspNetCore.Mvc;
using PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism;
using System.Threading.Tasks;

namespace PlagarismChecker.WebUI.Controllers
{
    public class PlagiarismController : ApiController
    {
        [HttpGet]
        public async Task<CheckTextForPlagiarismDto> Get()
        {
            var checkTextForPlagiarismDto = await Mediator.Send(new CheckTextForPlagiarismCommand { TextToSearch = "Block at first sight provides a way to detect and block new malware within seconds. This protection is enabled by default when certain prerequisite settings are enabled. These settings include cloud-delivered protection, a specified sample submission timeout (such as 50 seconds), and a file-blocking level of high. In most enterprise organizations, these settings are enabled by default with Microsoft Defender Antivirus deployments. bujar dervishaj qe jeton dhe vepron nga kosova etj etj." });
            return checkTextForPlagiarismDto;
        }
    }
}
