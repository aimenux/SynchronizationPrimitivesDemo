using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using App.Examples;
using App.Resource;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public static class Program
    {
        private const int MaxConcurrency = 5;

        public static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "DEV";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IPrinter, Printer>();
            services.AddTransient<IExample, Example01>();
            services.AddTransient<IExample, Example02>();
            services.AddTransient<IExample, Example03>();
            services.AddTransient<IExample, Example04>();
            services.AddTransient<IExample, Example05>();
            services.AddTransient<IExample, Example06>();
            services.AddTransient<IExample, Example07>();
            services.AddTransient<IExample, Example08>();
            services.AddTransient<IExample, Example09>();

            var serviceProvider = services.BuildServiceProvider();
            foreach (var example in serviceProvider.GetServices<IExample>())
            {
                ConsoleColor.Green.WriteLine($"\n{GetTitle(example)}");
                RunConcurrentCalls(example, MaxConcurrency);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            ConsoleColor.Gray.WriteLine("\nPress any key to exit !");
            Console.ReadKey();
        }

        private static void RunConcurrentCalls(IExample example, int max)
        {
            var threads = new List<Thread>();

            for (var index = 1; index <= max; index++)
            {
                var thread = new Thread(example.UsePrinter)
                {
                    Name = $"{index:0000}"
                };
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

        private static string GetTitle(IExample example)
        {
            return $"{example.GetType().Name} -> {example.Description}";
        }
    }
}
