using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Infrastructure.Persistence;
using PlagarismChecker.Infrastructure.Services;

namespace PlagarismChecker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlagiarismCheckerDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("PlagiarismCheckerDbConnection")));
            services.AddScoped<IPlagiarismCheckerDbContext>(provider => provider.GetService<PlagiarismCheckerDbContext>());


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ISearchEngineService, SearchEngineService>();


            return services;
        }
    }
}
