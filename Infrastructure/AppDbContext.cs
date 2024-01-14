using Application.Interfaces;
using Domain.Base;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Synonym> Synonyms => Set<Synonym>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SynonymConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            OnBeforeSaving();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            OnBeforeSaving();

            return base.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            var addedEntries = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is ITrackCreationTime creationTime)
                {
                    creationTime.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
