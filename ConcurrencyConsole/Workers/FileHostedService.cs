namespace ConcurrencyConsole.Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Concurrency.Application.File;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class FileHostedService : BackgroundService
    {
        private readonly IServiceProvider _fileUseCase;

        public FileHostedService(IServiceProvider fileUseCase)
        {
            _fileUseCase = fileUseCase;
            Console.WriteLine("Start FileHostedService");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var observationId = "32c9f2f6-b801-4250-abbc-9e0d217c1eb7";

            //Call the Use Case to save files and process information for observation and Files.
            using var serviceScope = _fileUseCase.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var fileUseCase = serviceProvider.GetRequiredService<IFileUseCase>();
            await fileUseCase.ProcessFileAsync(Guid.Parse(observationId));

            //Call directly the repository to update the Observation entity
            //var fileRepository = serviceProvider.GetRequiredService<IFile>();
            //for (int i = 0; i < 20; i++)
            //{
            //    await fileRepository.UpdateFileAsync(Guid.Parse(observationId));
            //}
        }
    }
}
