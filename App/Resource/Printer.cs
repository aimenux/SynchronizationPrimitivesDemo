using System;
using App.Exceptions;

namespace App.Resource
{
    public class Printer : IPrinter
    {
        public string Id { get; }
        public State State { get; private set; }

        public Printer()
        {
            Id = GetShortGuid();
            State = State.Ready;
        }

        public void Print(ConsoleColor color, string message)
        {
            if (State != State.Ready)
            {
                throw PrinterException.PrinterIsBusy(this);
            }

            State = State.Busy;
            color.WriteLine(message);
            State = State.Ready;
        }

        private static string GetShortGuid()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
