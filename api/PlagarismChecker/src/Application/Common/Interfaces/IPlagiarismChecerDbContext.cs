using Microsoft.EntityFrameworkCore;
using PlagarismChecker.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PlagarismChecker.Application.Common.Interfaces
{
    public interface IPlagiarismChecerDbContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
