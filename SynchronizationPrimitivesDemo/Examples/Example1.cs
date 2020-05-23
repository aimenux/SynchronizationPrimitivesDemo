using SynchronizationPrimitivesDemo.Resource;

namespace SynchronizationPrimitivesDemo.Examples
{
    public class Example1 : AbstractExample
    {
        private static readonly object Locker = new object();

        public Example1(IPrinter printer) : base(printer)
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
