using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example7 : AbstractExample
    {
        private static SpinLock _spinLock = new SpinLock(true);

        public Example7(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'spin lock' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            var lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);
                base.UsePrinter();
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }
    }
}