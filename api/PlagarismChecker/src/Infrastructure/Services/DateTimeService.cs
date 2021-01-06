using PlagarismChecker.Application.Common.Interfaces;
using System;

namespace PlagarismChecker.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
