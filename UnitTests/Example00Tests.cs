using System.Diagnostics;
using System.Threading.Tasks;
using App.Examples;
using NUnit.Framework;

namespace UnitTests
{
    public class Example00Tests
    {
        private const string EnableOnlyOnDebug = "DEBUG";

        [Test]
        public void Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V1()
        {
            GenericTestHelper.FailWithParallelActions<Example00>();
        }

        [Test]
        [Conditional(EnableOnlyOnDebug)]
        public void Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V2()
        {
            GenericTestHelper.FailWithParallelThreads<Example00>();
        }

        [Test]
        [Conditional(EnableOnlyOnDebug)]
        public void Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V3()
        {
            GenericTestHelper.FailWithParallelTasks<Example00>();
        }

        [Test]
        public Task Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V4()
        {
            return GenericTestHelper.FailWithParallelTasksAsync<Example00>();
        }
    }
}