using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Infrastructure.Files;
using PlagarismChecker.Infrastructure.Persistence;
using PlagarismChecker.Infrastructure.Services;

namespace PlagarismChecker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("PlagarismCheckerDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ISearchEngineService, SearchEngineService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();


            return services;
        }
    }
}
