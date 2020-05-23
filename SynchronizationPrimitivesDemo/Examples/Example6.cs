using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example6 : AbstractExample
    {
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1,1);

        public Example6(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'semaphore slim' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            SemaphoreSlim.Wait();
            base.UsePrinter();
            SemaphoreSlim.Release();
        }
    }
}
