namespace Concurrency.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DbContext;
    using Domain.Models;
    using Domain.RepositoryInterfaces;
    using Microsoft.EntityFrameworkCore;

    public class FileRepository : IFile
    {
        private readonly ConcurrencyContext _concurrencyContext;

        public FileRepository(ConcurrencyContext concurrencyContext)
        {
            _concurrencyContext = concurrencyContext;
            Console.WriteLine("New instance of FileRepository");
        }

        public async Task<int> SaveAsync(File file)
        {
            _concurrencyContext.Add(file);
            return await _concurrencyContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<File>> GetFileByObservationId(Guid observationId)
        {
            var files = await _concurrencyContext.Files.Include(f => f.FileMetadata)
                    .Where(f => f.FileMetadata.Any(fm =>
                            fm.Key == "ObservationId" && fm.Value.Equals(observationId.ToString()))).ToListAsync();
            return files;
        }

        public async Task UpdateFileAsync(Guid observationId)
        {
            var files = await _concurrencyContext.Files.Include(f => f.FileMetadata)
                    .Where(f => f.FileMetadata.Any(fm =>
                            fm.Key == "ObservationId" && fm.Value.Equals(observationId.ToString()))).ToListAsync();

           files.ForEach(f => f.ObservationId = observationId);
           _concurrencyContext.UpdateRange(files);

           await _concurrencyContext.SaveChangesAsync();

        }
    }
}
