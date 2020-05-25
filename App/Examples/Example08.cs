using System;
using System.Threading;
using App.Resource;

namespace App.Examples
{
    public class Example08 : AbstractExample
    {
        private static readonly ReaderWriterLock ReaderWriterLock = new ReaderWriterLock();

        public Example08(IPrinter printer) : base(printer)
        {
        }

        public override string Description { get; } = "Use 'reader writer lock' to set resource access limit to '1'";

        public override void UsePrinter()
        {
            try
            {
                var timeout = TimeSpan.FromSeconds(1);
                ReaderWriterLock.AcquireWriterLock(timeout);
                base.UsePrinter();
            }
            finally
            {
                ReaderWriterLock.ReleaseWriterLock();
            }
        }
    }
}