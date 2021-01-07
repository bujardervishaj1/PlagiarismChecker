namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism
{
    public class CheckTextForPlagiarismDto
    {
        public string PercentPlagiarized { get; set; }

        public string[] PlagarisedUrls { get; set; }
    }
}
