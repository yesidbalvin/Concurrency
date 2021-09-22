namespace ConcurrencyConsole
{
    using System;
    using System.IO;
    using Concurrency.Infrastructure.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceProvider = new ServiceCollection()
                    .AddRepositories(Configuration);
            
        }
    }
}
