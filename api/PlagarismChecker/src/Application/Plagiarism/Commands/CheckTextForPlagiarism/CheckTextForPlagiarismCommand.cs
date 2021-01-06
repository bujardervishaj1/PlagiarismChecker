using MediatR;
using PlagarismChecker.Application.Common.Helpers;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
        private int _plagiarized = 0;
        private int _numCorpuses = 0;

        public async Task<CheckTextForPlagiarismDto> Handle(CheckTextForPlagiarismCommand request, CancellationToken cancellationToken)
        {
            var sentences = request.TextToSearch.GetSentences();
            var plagiarism = CheckPlagiarism(sentences);

            float percentPlagiarized = (_plagiarized / (float)_numCorpuses) * 100;

            var checkTextForPlagiarismDto = new CheckTextForPlagiarismDto { NoOfPlagiarism = _plagiarized.ToString(), PercentPlagiarized = percentPlagiarized.ToString(), PlagarisedUrls = plagiarism };

            return checkTextForPlagiarismDto;
        }

        private string[] CheckPlagiarism(string[] sentences, bool isGoogle = false)
        {

            string[] res = new string[sentences.Length];
            this._plagiarized = 0;
            this._numCorpuses = sentences.Length;

            int i = 0;
            foreach (string sentence in sentences)
            {
                string r = "";
                r = this.CheckGoogle(sentence.Trim());

                if (r != "") this._plagiarized += 1;

                res[i] = r;

                i++;
            }

            return res;
        }

        private string CheckGoogle(string sentence)
        {

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["q"] = sentence;

            WebClient client = new WebClient();
            string content = client.DownloadString("https://www.google.com/search?hl=en&as_q=&as_epq=%22" + queryString.ToString().Replace("q=", "") + "%22");

            var t = content.CountOccurenceswWithinString(sentence);

            if (t <= 2)
                return "";

            return "https://www.google.com.sg/search?q=%22" + queryString.ToString().Replace("q=", "") + "%22";
        }

        private string CheckBing(string sentence)
        {

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["q"] = sentence;

            string content = "";
            if (queryString.ToString() != "q=+%0a%0a")
            {
                WebClient client = new WebClient();
                content = client.DownloadString("http://www.bing.com/search?q=\"" + queryString.ToString().Replace("q=", "") + "\"");
            }

            if (content.ToLower().Contains("no results found for")) return "";

            return "http://www.bing.com/search?q=\"" + queryString.ToString().Replace("q=", "") + "\"";
        }
    }
}
