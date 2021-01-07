using Microsoft.Extensions.Configuration;
using PlagarismChecker.Application.Common.Interfaces;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PlagarismChecker.Infrastructure.Services
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IConfiguration _configuration;
        private readonly string _engineUrl;
        private readonly string _cx;
        private readonly string _apiKey;

        public SearchEngineService(
             IConfiguration configuration)
        {
            this._configuration = configuration;
            var searchEnginSection = _configuration.GetSection("SearchEngine");
            this._engineUrl = searchEnginSection.GetValue<string>("Url");
            this._cx = searchEnginSection.GetValue<string>("CX");
            this._apiKey = searchEnginSection.GetValue<string>("ApiKey");
        }

        public async Task<string> SearchTheSentence(string sentence)
        {
            var request = WebRequest.Create(_engineUrl + _apiKey + "&cx=" + _cx + "&q=" + sentence);
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();

            return responseString;
        }
    }
}
