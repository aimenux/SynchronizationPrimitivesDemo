using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SynchronizationPrimitivesDemo.Examples;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo
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
            services.AddTransient<IExample, Example1>();
            services.AddTransient<IExample, Example2>();
            services.AddTransient<IExample, Example3>();
            services.AddTransient<IExample, Example4>();
            services.AddTransient<IExample, Example5>();
            services.AddTransient<IExample, Example6>();
            services.AddTransient<IExample, Example7>();
            services.AddTransient<IExample, Example8>();
            services.AddTransient<IExample, Example9>();

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
