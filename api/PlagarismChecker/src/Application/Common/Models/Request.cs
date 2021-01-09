using System.Collections.Generic;

namespace PlagarismChecker.Application.Common.Models
{
    public class Request
    {
        public string TextToCheck { get; set; }

        public List<string> ExcludedUrls { get; set; }
    }
}
