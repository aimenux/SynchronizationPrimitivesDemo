using App.Resource;

namespace App.Examples
{
    public class Example01 : AbstractExample
    {
        private static readonly object Locker = new object();

        public Example01(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'lock' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            lock (Locker)
            {
                base.UsePrinter();
            }
        }
    }
}
