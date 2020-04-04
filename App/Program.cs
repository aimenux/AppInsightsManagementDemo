using System;
using System.IO;
using App.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class Program
    {
        private const string DefaultEnvironmentToUse = "DEV";
        
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? DefaultEnvironmentToUse;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IApiKeyProvider, ApiKeyProvider>();
            services.Configure<Settings>(configuration.GetSection(nameof(Settings)));

            var serviceProvider = services.BuildServiceProvider();

            using (var apiKeyProvider = serviceProvider.GetRequiredService<IApiKeyProvider>())
            {
                var apiKey = apiKeyProvider.Create();

                var isCreatedMessage = apiKey != null
                    ? $"ApiKey '{apiKey.Name}' is created"
                    : "ApiKey is not created";

                Console.WriteLine(isCreatedMessage);

                if (apiKey != null)
                {
                    ConsoleColor.Yellow.PressAnyKeyToContinue();

                    var isDeletedMessage = apiKeyProvider.Delete(apiKey)
                        ? $"ApiKey '{apiKey.Name}' is deleted"
                        : $"ApiKey '{apiKey.Name}' is not deleted";

                    Console.WriteLine(isDeletedMessage);
                }
            }

            ConsoleColor.Yellow.PressAnyKeyToExit();
        }
    }
}
