using System;

namespace PlagarismChecker.Application.Common.Models
{
    public class SearchHistory
    {
        public Request Request { get; set; }

        public Result Result { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
