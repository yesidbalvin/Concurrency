namespace ConcurrencyConsole.Workers
{
    using System;
    using System.Threading.Tasks;
    using Concurrency.Application.Observation;

    public class ObservationWorker : IObservationWorker
    {
        private readonly IObservationUseCase _service;

        public ObservationWorker(IObservationUseCase observationUseCase)
        {
            _service = observationUseCase;
        }

        public async Task ExecuteAsync(Guid observationId)
        {
            await _service.ProcessObservationAsync(observationId);
        }
    }

    public interface IObservationWorker
    {
        Task ExecuteAsync(Guid observationId);
    }
}
