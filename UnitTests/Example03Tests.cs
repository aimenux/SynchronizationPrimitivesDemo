using App.Examples;
using NUnit.Framework;

namespace UnitTests
{
    public class Example03Tests
    {
        [Test]
        public void Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V1()
        {
            GenericTestHelper.PassWithParallelActions<Example03>();
        }

        [Test]
        public void Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V2()
        {
            GenericTestHelper.PassWithParallelThreads<Example03>();
        }

        [Test]
        public void Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V3()
        {
            GenericTestHelper.PassWithParallelTasks<Example03>();
        }
    }
}