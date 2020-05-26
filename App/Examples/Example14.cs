using System.Threading.Tasks;
using App.Resource;
using Nito.AsyncEx;

namespace App.Examples
{
    public class Example14 : AbstractExample
    {
        private static readonly AsyncReaderWriterLock AsyncReaderWriterLock = new AsyncReaderWriterLock();

        public Example14(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'async reader writer lock' to set resource access limit to '1'";

        public override async Task UsePrinterAsync()
        {
            using (await AsyncReaderWriterLock.WriterLockAsync())
            {
                await base.UsePrinterAsync();
            }
        }
    }
}