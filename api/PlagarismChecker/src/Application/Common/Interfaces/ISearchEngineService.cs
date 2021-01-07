using System.Threading.Tasks;

namespace PlagarismChecker.Application.Common.Interfaces
{
    public interface ISearchEngineService
    {
        public Task<string> SearchTheSentence(string sentence);
    }
}
