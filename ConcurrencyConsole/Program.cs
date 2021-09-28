namespace ConcurrencyConsole
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Concurrency.Application.Extensions;
    using Concurrency.Infrastructure.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Workers;

    public class Program
    {
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                // Needed to inject config server URI and token to the pods
                .AddEnvironmentVariables()
                // Needed to have the env variables at the top of the configuration hierarchy
                .AddEnvironmentVariables()
                .Build();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Create 2 workers as HostedService
            var host = CreatedBuilder(args).Build();

            //Using 2 scope, it didn´t work
            //
            //var host = CreateHostBuilderWithSeparatedScopes(args).Build();
            //await SaveObservationWithFiles(host.Services);
            //
            await host.RunAsync();
        }

        private static IHostBuilder CreatedBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services)=>
                            services.AddHostedService<FileHostedService>()
                                    .AddHostedService<ObservationHostedService>()
                                    .AddRepositories(Configuration)
                                    .AddUseCases()
                            );
        }

        private static IHostBuilder CreateHostBuilderWithSeparatedScopes(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                            services.AddSingleton<IFileWorker, FileWorker>()
                                    .AddSingleton<IObservationWorker, ObservationWorker>()
                                    .AddRepositories(Configuration)
                                    .AddUseCases()
                    );
        }

        private static async Task SaveObservationWithFiles(IServiceProvider services)
        {
            var observationId = Guid.NewGuid();
            //Same Scope, this implementation didn´t work

            using var serviceScope = services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var observationWorker = serviceProvider.GetRequiredService<IObservationWorker>();
            var observationTask = observationWorker.ExecuteAsync(observationId);


            using var serviceScopeFile = services.CreateScope();
            var serviceProviderFile = serviceScopeFile.ServiceProvider;
            var fileWorker = serviceProviderFile.GetRequiredService<IFileWorker>();
            var fileTask = fileWorker.ExecuteAsync(observationId);

            await Task.WhenAll(observationTask, fileTask);
        }
    }
}
