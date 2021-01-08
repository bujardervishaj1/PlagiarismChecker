using PlagarismChecker.Application.Common.Mappings;
using PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism;

namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarismForUser
{
    public class CheckTextForPlagiarismForUserDto : IMapFrom<CheckTextForPlagiarismDto>
    {
        public string Username { get; set; }

        public string PercentPlagiarized { get; set; }

        public string[] PlagarisedUrls { get; set; }
    }
}
