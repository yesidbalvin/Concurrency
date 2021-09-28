namespace ConcurrencyConsole.Workers
{
    using System;
    using System.Threading.Tasks;
    using Concurrency.Application.File;

    public class FileWorker : IFileWorker
    {
        private readonly IFileUseCase _service;

        public FileWorker(IFileUseCase service)
        {
            _service = service;
        }

        public async Task ExecuteAsync(Guid observationId)
        {
            await _service.ProcessFileAsync(observationId);
        }
    }

    public interface IFileWorker
    {
        Task ExecuteAsync(Guid observationId);
    }
}
