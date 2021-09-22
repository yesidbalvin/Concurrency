namespace Concurrency.Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using DbContext;
    using Domain.Models;
    using Domain.RepositoryInterfaces;

    public class ObservationRepository : IObservation
    {
        private readonly ConcurrencyContext _concurrencyContext;

        public ObservationRepository(ConcurrencyContext concurrencyContext)
        {
            _concurrencyContext = concurrencyContext;
        }

        public async Task<int> SaveAsync(Observation observation)
        {
            _concurrencyContext.Add(observation);
            return await _concurrencyContext.SaveChangesAsync();
        }
    }
}
