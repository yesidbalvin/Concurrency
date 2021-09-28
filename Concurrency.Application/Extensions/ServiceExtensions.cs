namespace Concurrency.Application.Extensions
{
    using File;
    using Microsoft.Extensions.DependencyInjection;
    using Observation;
    using Services;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services.AddScoped<IObservationUseCase, ObservationUseCase>()
                    .AddScoped<IFileUseCase, FileUseCase>()
                    .AddScoped<ILinkObservationFileService, LinkObservationFileService>();
        }
    }
}
