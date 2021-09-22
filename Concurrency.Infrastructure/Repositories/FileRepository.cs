namespace Concurrency.Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using DbContext;
    using Domain.Models;
    using Domain.RepositoryInterfaces;

    public class FileRepository : IFile
    {
        private readonly ConcurrencyContext _concurrencyContext;

        public FileRepository(ConcurrencyContext concurrencyContext)
        {
            _concurrencyContext = concurrencyContext;
        }

        public async Task<int> SaveAsync(File file)
        {
            _concurrencyContext.Add(file);
            return await _concurrencyContext.SaveChangesAsync();
        }
    }
}
