using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example5 : AbstractExample
    {
        private static readonly Semaphore Semaphore = new Semaphore(1,1);

        public Example5(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'semaphore' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            Semaphore.WaitOne();
            base.UsePrinter();
            Semaphore.Release();
        }
    }
}
