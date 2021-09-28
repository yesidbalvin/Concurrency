namespace Concurrency.Application.Observation
{
    using System;
    using System.Threading.Tasks;
    using Domain.Models;
    using Domain.RepositoryInterfaces;
    using Services;

    public class ObservationUseCase : IObservationUseCase
    {
        private readonly IObservation _observationRepository;
        private readonly ILinkObservationFileService _linkObservationFileService;

        public ObservationUseCase(IObservation observation, ILinkObservationFileService linkObservationFileService)
        {
            _observationRepository = observation;
            _linkObservationFileService = linkObservationFileService;
            Console.WriteLine("New Instance of ObservationUseCase");
        }


        public async Task<Guid> ProcessObservationAsync(Guid observationId)
        {
            var observation = new Observation
            {
                    Id = observationId
            };

            var observationResult = await _observationRepository.SaveAsync(observation);
            //CAll LinkObservationUseCase
            await _linkObservationFileService.ProcessAsync(observationId);
            return observationId;
        }
    }
}
