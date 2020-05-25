using System.Threading;
using App.Resource;

namespace App.Examples
{
    public class Example02 : AbstractExample
    {
        private static readonly object Locker = new object();

        public Example02(IPrinter printer) : base(printer)
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
