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
        private readonly string _apiKey1;
        private readonly string _apiKey2;
        private readonly string _apiKey3;
        private readonly string _apiKey4;
        private readonly string _apiKey5;

        public SearchEngineService(
             IConfiguration configuration)
        {
            this._configuration = configuration;
            var searchEnginSection = _configuration.GetSection("SearchEngine");
            this._engineUrl = searchEnginSection.GetValue<string>("Url");
            this._cx = searchEnginSection.GetValue<string>("CX");
            this._apiKey1 = searchEnginSection.GetValue<string>("ApiKey1");
            this._apiKey2 = searchEnginSection.GetValue<string>("ApiKey2");
            this._apiKey3 = searchEnginSection.GetValue<string>("ApiKey3");
            this._apiKey4 = searchEnginSection.GetValue<string>("ApiKey4");
            this._apiKey5 = searchEnginSection.GetValue<string>("ApiKey5");
        }

        public async Task<string> SearchTheSentenceApi(string sentence, int apiNo)
        {
            string apiKey;
            switch (apiNo)
            {
                case 1:
                case 2:
                    apiKey = _apiKey2;
                    break;
                case 3:
                    apiKey = _apiKey3;
                    break;
                case 4:
                    apiKey = _apiKey4;
                    break;
                case 5:
                default:
                    apiKey = _apiKey5;
                    break;
            }

            return await SearchTheSentence(sentence, apiKey);
        }

        private async Task<string> SearchTheSentence(string sentence, string apiKey)
        {
            var request = WebRequest.Create(_engineUrl + apiKey + "&cx=" + _cx + "&q=" + sentence);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();

            return responseString;
        }
    }
}
