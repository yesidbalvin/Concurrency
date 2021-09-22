namespace Concurrency.Infrastructure.DbContext
{
    using System;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class ConcurrencyContext : DbContext
    {
        public ConcurrencyContext(DbContextOptions<ConcurrencyContext> options) : base(options)
        {
        }

        public DbSet<Observation> Observations { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Metadata> MetadataFile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));
        }
    }
}
