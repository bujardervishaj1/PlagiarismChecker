using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Common.Interfaces
{
    public interface IPlagiarismCheckerDbContext
    {
        DbSet<User> Users { get; set; }

        DbSet<UserHistory> UserHistory { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
