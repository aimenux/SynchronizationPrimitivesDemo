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
        public const int MaxConcurrency = 20;

        public static void PassWithParallelActions<T>() where T : IExample
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
                FailWhenConcurrencyException(ex);
            }
        }

        public static void PassWithParallelTasks<T>() where T : IExample
        {
            var example = CreateExample<T>();
            var exceptions = new ConcurrentQueue<Exception>();
            var tasks = Enumerable.Range(1, MaxConcurrency)
                .Select(x => Task.Run(() =>
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

            Task.WaitAll(tasks);

            exceptions.Should().BeEmpty();
        }

        public static async Task PassWithParallelTasksAsync<T>() where T : IExample
        {
            try
            {
                var example = CreateExample<T>();
                var tasks = Enumerable.Range(1, MaxConcurrency)
                    .Select(x => Task.Run(async() => await example.UsePrinterAsync()))
                    .ToArray();

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                FailWhenConcurrencyException(ex);
            }
        }

        public static void PassWithParallelThreads<T>() where T : IExample
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

        public static void FailWithParallelActions<T>() where T : IExample
        {
            try
            {
                var example = CreateExample<T>();
                Parallel.For(0, MaxConcurrency, index =>
                {
                    example.UsePrinter();
                });

                Assert.Fail("Concurrency exception not thrown");
            }
            catch (Exception ex)
            {
                PassWhenConcurrencyException(ex);
            }
        }

        public static void FailWithParallelTasks<T>() where T : IExample
        {
            var example = CreateExample<T>();
            var exceptions = new ConcurrentQueue<Exception>();
            var tasks = Enumerable.Range(1, MaxConcurrency)
                .Select(x => Task.Run(() =>
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

            Task.WaitAll(tasks);

            exceptions.Should().NotBeEmpty();
            exceptions.Should().AllBeOfType<PrinterException>();
        }

        public static void FailWithParallelThreads<T>() where T : IExample
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

            exceptions.Should().NotBeEmpty();
            exceptions.Should().AllBeOfType<PrinterException>();
        }

        public static void FailWhenConcurrencyException(Exception ex)
        {
            var exception = ex?.InnerException ?? ex;
            exception.Should().BeOfType<PrinterException>();
            Assert.Fail($"Concurrency exception ({ex?.Message})");
        }

        public static void PassWhenConcurrencyException(Exception ex)
        {
            var exception = ex?.InnerException ?? ex;
            exception.Should().BeOfType<PrinterException>();
            Assert.Pass($"Concurrency exception ({ex?.Message})");
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