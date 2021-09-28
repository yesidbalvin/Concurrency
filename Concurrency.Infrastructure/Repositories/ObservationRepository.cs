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

    public class ObservationRepository : IObservation
    {
        private readonly ConcurrencyContext _concurrencyContext;

        public ObservationRepository(ConcurrencyContext concurrencyContext)
        {
            _concurrencyContext = concurrencyContext;
            Console.WriteLine("New instance of ObservationRepository");
        }

        public async Task<int> SaveAsync(Observation observation)
        {
            if (!_concurrencyContext.Observations.Any(o => o.Id == observation.Id))
            {
                _concurrencyContext.Add(observation);
            }
            
            return await _concurrencyContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Guid observationId, IEnumerable<File> files)
        {
            var result = 0;
            try
            {
                var observation = _concurrencyContext.Observations.Include(o => o.Files)
                        .FirstOrDefault(o => o.Id == observationId);

                files.ToList().ForEach(file => observation.Files.Add(file));

                result = await _concurrencyContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }


        public async Task<Observation> GetObservationById(Guid observationId)
        {
            return await _concurrencyContext.Observations.SingleAsync(o => o.Id == observationId);
        }
    }
}
