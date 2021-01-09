using MediatR;
using Newtonsoft.Json;
using PlagarismChecker.Application.Common.Helpers;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism
{
    public class CheckTextForPlagiarismCommand : IRequest<CheckTextForPlagiarismDto>
    {
        public CheckTextForPlagiarismCommand()
        {
            ExcludedUrls = new List<string>();
        }

        public string TextToCheck { get; set; }

        public List<string> ExcludedUrls { get; set; }
    }

    public class CheckTextForPlagiarismCommandHandler : IRequestHandler<CheckTextForPlagiarismCommand, CheckTextForPlagiarismDto>
    {
        private readonly ISearchEngineService _searchEngineService;

        public CheckTextForPlagiarismCommandHandler(
            ISearchEngineService searchEngineService
            )
        {
            this._searchEngineService = searchEngineService;
        }

        public async Task<CheckTextForPlagiarismDto> Handle(CheckTextForPlagiarismCommand request, CancellationToken cancellationToken)
        {
            var sentences = request.TextToCheck.GetSentences();
            var plagiarism = await CheckPlagiarism(sentences, request.ExcludedUrls);

            float percentPlagiarized = (_plagiarized / (float)_numSentences) * 100;

            var checkTextForPlagiarismDto = new CheckTextForPlagiarismDto { PercentPlagiarized = percentPlagiarized.ToString(), PlagarisedUrls = plagiarism };

            return checkTextForPlagiarismDto;
        }


        private int _plagiarized = 0;
        private int _numSentences = 0;
        private async Task<string[]> CheckPlagiarism(string[] sentences, List<string> excludedUrls)
        {
            var res = new List<string>();
            this._plagiarized = 0;
            this._numSentences = sentences.Length;

            foreach (string sentence in sentences)
            {
                string r = "";
                r = await this.CheckGoogleProgramableEngine(sentence.Trim(), excludedUrls);

                if (r != "")
                {
                    this._plagiarized += 1;
                    res.Add(r);
                }
            }

            return res.Distinct().ToArray();
        }

        private async Task<string> CheckGoogleProgramableEngine(string sentence, List<string> excludedUrls)
        {
            string responseString = await _searchEngineService.SearchTheSentence(sentence);

            dynamic jsonData = JsonConvert.DeserializeObject(responseString);
            var results = new List<SearchEnginResult>();

            try
            {
                foreach (var item in jsonData.items)
                {
                    results.Add(new SearchEnginResult
                    {
                        Title = item.title,
                        Link = item.link,
                        Snippet = item.snippet,
                    });
                }
                foreach (var result in results)
                {
                    if (result.Snippet.Replace("...", "").Replace("\n", "").Contains(sentence))
                    {
                        for (int i = 0; i < excludedUrls.Count; i++)
                        {
                            if (excludedUrls[i] == result.Link)
                            {
                                return "";
                            }
                        }
                        return result.Link;
                    }
                }
            }
            catch { return ""; }

            return "";
        }
    }
}
