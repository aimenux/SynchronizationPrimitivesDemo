using App.Resource;

namespace App.Examples
{
    public class Example00 : AbstractExample
    {
        public Example00(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Bad example without any synchronization primitive";
    }
}
