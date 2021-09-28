namespace Concurrency.Domain.RepositoryInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IObservation
    {
        Task<int> SaveAsync(Observation observation);
        Task<int> UpdateAsync(Guid observationId, IEnumerable<File> files);
        Task<Observation> GetObservationById(Guid observationId);
    }
}
