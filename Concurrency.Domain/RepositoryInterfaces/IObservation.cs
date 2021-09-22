namespace Concurrency.Domain.RepositoryInterfaces
{
    using System.Threading.Tasks;
    using Models;

    public interface IObservation
    {
        Task<int> SaveAsync(Observation observation);
    }
}
