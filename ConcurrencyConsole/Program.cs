namespace ConcurrencyConsole
{
    using Concurrency.Infrastructure.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;

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
            var host = CreateHostBuilder(args).Build();
            host.Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            services.AddRepositories(Configuration));
    }
}
