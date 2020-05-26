using System.Threading.Tasks;
using App.Resource;
using Nito.AsyncEx;

namespace App.Examples
{
    public class Example12 : AbstractExample
    {
        private static readonly AsyncLock AsyncLock = new AsyncLock();

        public Example12(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'async lock' to set resource access limit to '1'";

        public override async Task UsePrinterAsync()
        {
            using (await AsyncLock.LockAsync())
            {
                await base.UsePrinterAsync();
            }
        }
    }
}