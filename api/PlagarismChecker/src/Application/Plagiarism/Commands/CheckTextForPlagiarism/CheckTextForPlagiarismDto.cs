namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism
{
    public class CheckTextForPlagiarismDto
    {
        public string NoOfPlagiarism { get; set; }

        public string PercentPlagiarized { get; set; }

        public string[] PlagarisedUrls { get; set; }
    }
}
