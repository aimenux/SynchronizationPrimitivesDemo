using System.Threading.Tasks;
using App.Resource;
using Nito.AsyncEx;

namespace App.Examples
{
    public class Example13 : AbstractExample
    {
        private static readonly AsyncSemaphore AsyncSemaphore = new AsyncSemaphore(1);

        public Example13(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'async semaphore' to set resource access limit to '1'";

        public override async Task UsePrinterAsync()
        {
            try
            {
                await AsyncSemaphore.WaitAsync();
                await base.UsePrinterAsync();
            }
            finally
            {
                AsyncSemaphore.Release();
            }
        }
    }
}