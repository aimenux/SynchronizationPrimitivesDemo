using System.Threading;
using App.Resource;

namespace App.Examples
{
    public class Example04 : AbstractExample
    {
        private static readonly Mutex Mutex = new Mutex();

        public Example04(IPrinter printer) : base(printer)
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
