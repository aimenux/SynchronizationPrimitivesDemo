using App.Examples;
using NUnit.Framework;

namespace UnitTests
{
    public class Example06Tests
    {
        [Test]
        public void Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V1()
        {
            GenericTestHelper.PassWithParallelActions<Example06>();
        }

        [Test]
        public void Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer_V2()
        {
            GenericTestHelper.PassWithParallelThreads<Example06>();
        }
    }
}