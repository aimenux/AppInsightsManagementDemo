using System;
using System.IO;
using App.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class Program
    {
        public static void Main()
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.Configure<Settings>(configuration.GetSection(nameof(Settings)));
            services.AddSingleton<IApiKeyProvider, ApiKeyProvider>();

            var serviceProvider = services.BuildServiceProvider();

            using (var apiKeyProvider = serviceProvider.GetRequiredService<IApiKeyProvider>())
            {
                var apiKeyCreated = apiKeyProvider.Create();

                Console.WriteLine($"ApiKey Id: {apiKeyCreated.Id}");
                Console.WriteLine($"ApiKey Name: {apiKeyCreated.Name}");
                Console.WriteLine($"ApiKey CreatedDate: {apiKeyCreated.CreatedDate}");
            }

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
    }
}
