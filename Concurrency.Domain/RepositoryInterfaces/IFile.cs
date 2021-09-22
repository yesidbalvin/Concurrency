namespace Concurrency.Domain.RepositoryInterfaces
{
    using System.Threading.Tasks;
    using Models;

    public interface IFile
    {
        Task<int> SaveAsync(File file);
    }
}
