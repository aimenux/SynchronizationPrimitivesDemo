using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example3 : AbstractExample
    {
        private static readonly object Locker = new object();

        public Example3(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'monitor v2' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            var lockTaken = false;
            Monitor.Enter(Locker, ref lockTaken);
            try
            {
                base.UsePrinter();
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(Locker);
                }
            }
        }
    }
}
