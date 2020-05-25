using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Examples;
using App.Exceptions;
using App.Resource;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    public static class GenericTestHelper
    {
        private const int MaxConcurrency = 10;

        public static void RunWithParallelActions<T>() where T : IExample
        {
            try
            {
                var example = CreateExample<T>();
                Parallel.For(0, MaxConcurrency, index =>
                {
                    example.UsePrinter();
                });
            }
            catch (Exception ex)
            {
                AssertConcurrencyIsFailed(ex);
            }
        }

        public static void RunWithParallelTasks<T>() where T : IExample
        {
            try
            {
                var example = CreateExample<T>();
                var tasks = Enumerable.Range(1, MaxConcurrency)
                    .Select(x => new Task(example.UsePrinter))
                    .ToArray();

                Parallel.ForEach(tasks, task => task.Start());

                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                AssertConcurrencyIsFailed(ex);
            }
        }

        public static void RunWithParallelThreads<T>() where T : IExample
        {
            var example = CreateExample<T>();
            var exceptions = new ConcurrentQueue<Exception>();
            var threads = Enumerable.Range(1, MaxConcurrency)
                .Select(x => new Thread(() =>
                {
                    try
                    {
                        example.UsePrinter();
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }
                })).ToArray();

            Parallel.ForEach(threads, thread => thread.Start());

            foreach (var thread in threads)
            {
                thread.Join();
            }

            exceptions.Should().BeEmpty();
        }

        public static void AssertConcurrencyIsFailed(Exception ex)
        {
            var exception = ex?.InnerException ?? ex;
            exception.Should().BeOfType<PrinterException>();
            Assert.Fail($"Concurrency is failed ({ex?.Message})");
        }

        private static T CreateExample<T>() where T : IExample
        {
            var example = (T) Activator.CreateInstance(typeof(T), args: new Printer());
            if (example == null)
            {
                throw new Exception($"Failed to create instance of type '{typeof(T)}'");
            }

            return example;
        }
    }
}