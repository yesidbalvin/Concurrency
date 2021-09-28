namespace Concurrency.Application.Observation
{
    using System;
    using System.Threading.Tasks;

    public interface IObservationUseCase
    {
        Task<Guid> ProcessObservationAsync(Guid observationId);
    }
}
