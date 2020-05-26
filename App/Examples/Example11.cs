using System.Threading.Tasks;
using App.Resource;
using Nito.AsyncEx;

namespace App.Examples
{
    public class Example11 : AbstractExample
    {
        private static readonly AsyncMonitor AsyncMonitor = new AsyncMonitor();

        public Example11(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'async monitor' to set resource access limit to '1'";

        public override async Task UsePrinterAsync()
        {
            using (await AsyncMonitor.EnterAsync())
            {
                await base.UsePrinterAsync();
            }
        }
    }
}