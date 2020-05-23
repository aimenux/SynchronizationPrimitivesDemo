using System.Threading;
using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example2 : AbstractExample
    {
        private static readonly object Locker = new object();

        public Example2(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'monitor v1' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            Monitor.Enter(Locker);
            try
            {
                base.UsePrinter();
            }
            finally
            {
                Monitor.Exit(Locker);
            }
        }
    }
}
