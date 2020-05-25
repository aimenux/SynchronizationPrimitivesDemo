using System;
using System.Linq;
using System.Threading.Tasks;
using App.Examples;
using App.Exceptions;
using App.Resource;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    public class Example10Tests
    {
        private const int MaxConcurrency = 10;

        [Test]
        public async Task Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer()
        {
            try
            {
                var example = new Example10(new Printer());
                var tasks = Enumerable.Range(1, MaxConcurrency)
                    .Select(x => Task.Run(async() => await example.UsePrinterAsync()))
                    .ToArray();

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                GenericTestHelper.AssertConcurrencyIsFailed(ex);
            }
        }
    }
}