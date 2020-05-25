using System.Threading;
using System.Threading.Tasks;
using App.Resource;

namespace App.Examples
{
    public class Example10 : AbstractExample
    {
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1,1);

        public Example10(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'semaphore slim' to set resource access limit to '1'";

        public override async Task UsePrinterAsync()
        {
            await SemaphoreSlim.WaitAsync();
            await base.UsePrinterAsync();
            SemaphoreSlim.Release();
        }
    }
}