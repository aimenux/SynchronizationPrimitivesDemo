using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public abstract class AbstractExample : IExample
    {
        protected readonly IPrinter Printer;

        protected AbstractExample(IPrinter printer)
        {
            Printer = printer;
        }

        protected static string ThreadName => $"{Thread.CurrentThread.GetThreadName()}";

        public abstract string Description { get; }

        public virtual void UsePrinter()
        {
            var printerId = Printer.Id;
            var color = Extensions.RandomColor();
            color.WriteLine($"\nThread [Name={ThreadName}] is entering the critical section");
            Printer.Print(color, $"Thread [Name={ThreadName}] is using the printer [Id={printerId}]");
            color.WriteLine($"Thread [Name={ThreadName}] is exiting the critical section");
        }
    }
}
