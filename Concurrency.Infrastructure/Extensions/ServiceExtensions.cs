namespace Concurrency.Infrastructure.Extensions
{
    using DbContext;
    using Domain.RepositoryInterfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repositories;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MariaDb:DefaultConnection");
            services.AddDbContext<ConcurrencyContext>(options =>
                            options.UseMySql(connectionString,
                                    ServerVersion.AutoDetect(connectionString),
                                    b => b.MigrationsAssembly(typeof(ServiceExtensions).Assembly.GetName().FullName)
                                            .EnableRetryOnFailure()
                                            
                            ).EnableSensitiveDataLogging() )
                    .AddScoped<IObservation, ObservationRepository>()
                    .AddScoped<IFile, FileRepository>();
            return services;
        }
    }
}
