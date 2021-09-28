namespace Concurrency.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using Domain.RepositoryInterfaces;

    public class LinkObservationFileService : ILinkObservationFileService
    {
        private readonly IFile _file;

        private readonly IObservation _observationRepository;

        public LinkObservationFileService(IObservation observation, IFile file)
        {
            _observationRepository = observation;
            _file = file;
            Console.WriteLine("New instance of LinkObservationFileService");
        }

        public async Task ProcessAsync(Guid observationId)
        {
            var observation = await _observationRepository.GetObservationById(observationId);
            var fileList = await _file.GetFileByObservationId(observationId);

            Console.WriteLine("Entity Updated-------------->");
            await _observationRepository.UpdateAsync(observation.Id, fileList);
        }
    }
}
