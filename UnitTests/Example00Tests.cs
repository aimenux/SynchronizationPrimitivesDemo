using App.Examples;
using NUnit.Framework;

namespace UnitTests
{
    public class Example00Tests
    {
        [Test]
        public void Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V1()
        {
            GenericTestHelper.FailWithParallelActions<Example00>();
        }

        [Test]
        public void Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V2()
        {
            GenericTestHelper.FailWithParallelTasks<Example00>();
        }

        [Test]
        public void Should_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V3()
        {
            GenericTestHelper.FailWithParallelThreads<Example00>();
        }
    }
}