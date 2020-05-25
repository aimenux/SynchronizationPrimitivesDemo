using System.Threading;
using App.Resource;

namespace App.Examples
{
    public class Example05 : AbstractExample
    {
        private static readonly Semaphore Semaphore = new Semaphore(1,1);

        public Example05(IPrinter printer) : base(printer)
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
