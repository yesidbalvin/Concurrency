namespace ConcurrencyConsole.Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Concurrency.Application.Observation;
    using Concurrency.Domain.RepositoryInterfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class ObservationHostedService : BackgroundService
    {
        private readonly IServiceProvider _observationUseCase;
        //private readonly IObservationUseCase _observationUseCase;

        public ObservationHostedService(IServiceProvider observationUseCase)
        {
            _observationUseCase = observationUseCase;
            Console.WriteLine("Start ObservationHostedService");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var observationId = "32c9f2f6-b801-4250-abbc-9e0d217c1eb7";
            
            //Call the Use Case to save Observation and process information for observation and Files.
            using var serviceScope = _observationUseCase.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var observationUseCase = serviceProvider.GetRequiredService<IObservationUseCase>();
            await observationUseCase.ProcessObservationAsync(Guid.Parse(observationId));

            //Call directly the repository to update the Observation entity
            //var fileRepository = serviceProvider.GetRequiredService<IFile>();
            //for (int i = 0; i < 20; i++)
            //{
            //    await fileRepository.UpdateFileAsync(Guid.Parse(observationId));
            //}

        }
    }
}
