using System.Threading;
using App.Resource;

namespace App.Examples
{
    public class Example06 : AbstractExample
    {
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1,1);

        public Example06(IPrinter printer) : base(printer)
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
