using MediatR;
using Newtonsoft.Json;
using PlagarismChecker.Application.Common.Helpers;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Application.Common.Models;
using PlagarismChecker.Application.Users.Queries.GetUser;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism
{
    public class CheckTextForPlagiarismCommand : IRequest<CheckTextForPlagiarismDto>
    {
        public string TextToSearch { get; set; }
    }

    public class CheckTextForPlagiarismCommandHandler : IRequestHandler<CheckTextForPlagiarismCommand, CheckTextForPlagiarismDto>
    {
        private readonly ISearchEngineService _searchEngineService;
        private readonly IRequestHandler<GetUserQuery, GetUserDto> _getUser;

        public CheckTextForPlagiarismCommandHandler(
            ISearchEngineService searchEngineService,
            IRequestHandler<GetUserQuery, GetUserDto> getUser
            )
        {
            this._searchEngineService = searchEngineService;
            this._getUser = getUser;
        }

        public async Task<CheckTextForPlagiarismDto> Handle(CheckTextForPlagiarismCommand request, CancellationToken cancellationToken)
        {
            var sentences = request.TextToSearch.GetSentences();
            var plagiarism = await CheckPlagiarism(sentences);

            float percentPlagiarized = (_plagiarized / (float)_numSentences) * 100;

            var checkTextForPlagiarismDto = new CheckTextForPlagiarismDto { PercentPlagiarized = percentPlagiarized.ToString(), PlagarisedUrls = plagiarism };

            return checkTextForPlagiarismDto;
        }


        private int _plagiarized = 0;
        private int _numSentences = 0;
        private async Task<string[]> CheckPlagiarism(string[] sentences)
        {
            var res = new List<string>();
            this._plagiarized = 0;
            this._numSentences = sentences.Length;

            foreach (string sentence in sentences)
            {
                string r = "";
                r = await this.CheckGoogleProgramableEngine(sentence.Trim());

                if (r != "")
                {
                    this._plagiarized += 1;
                    res.Add(r);
                }
            }

            return res.Distinct().ToArray();
        }

        private async Task<string> CheckGoogleProgramableEngine(string sentence)
        {
            string responseString = await _searchEngineService.SearchTheSentence(sentence);

            dynamic jsonData = JsonConvert.DeserializeObject(responseString);
            var results = new List<Result>();

            try
            {
                foreach (var item in jsonData.items)
                {
                    results.Add(new Result
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
                        return result.Link;
                    }
                }
            }
            catch { return ""; }

            return "";
        }
    }
}
