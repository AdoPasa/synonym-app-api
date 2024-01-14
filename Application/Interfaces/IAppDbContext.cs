using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Synonym> Synonyms { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
