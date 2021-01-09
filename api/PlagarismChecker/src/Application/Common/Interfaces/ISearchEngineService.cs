using System.Threading.Tasks;

namespace PlagarismChecker.Application.Common.Interfaces
{
    public interface ISearchEngineService
    {
        public Task<string> SearchTheSentenceApi(string sentence, int apiNo);
    }
}
