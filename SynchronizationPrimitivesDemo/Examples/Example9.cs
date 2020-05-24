using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example9 : AbstractExample
    {
        private static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();

        public Example9(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'reader writer lock slim' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            try
            {
                ReaderWriterLockSlim.EnterWriteLock();
                base.UsePrinter();
            }
            finally
            {
                ReaderWriterLockSlim.ExitWriteLock();
            }
        }
    }
}