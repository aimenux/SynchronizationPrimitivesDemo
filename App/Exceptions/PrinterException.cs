using System;
using System.Runtime.Serialization;
using App.Resource;

namespace App.Exceptions
{
    [Serializable]
    public class PrinterException : ApplicationException
    {
        protected PrinterException()
        {
        }
        protected PrinterException(string message) : base(message)
        {
        }
        protected PrinterException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected PrinterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public static PrinterException PrinterIsBusy(IPrinter printer)
        {
            return new PrinterException($"Printer '{printer.Id}' is busy");
        }
    }
}
