using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example4 : AbstractExample
    {
        private static readonly Mutex Mutex = new Mutex();

        public Example4(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'mutex' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            Mutex.WaitOne();
            base.UsePrinter();
            Mutex.ReleaseMutex();
        }
    }
}
