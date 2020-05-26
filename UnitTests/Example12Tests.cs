using System.Threading.Tasks;
using App.Examples;
using NUnit.Framework;

namespace UnitTests
{
    public class Example12Tests
    {
        [Test]
        public Task Should_Not_Throw_Printer_Exception_When_Multiple_Clients_Try_To_Use_Printer()
        {
            return GenericTestHelper.PassWithParallelTasksAsync<Example12>();
        }
    }
}