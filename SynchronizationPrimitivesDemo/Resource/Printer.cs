using System;

namespace SynchronizationPrimitivesDemo.Resource
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
                ConsoleColor.Red.WriteLine("An error has occured due to busy state");
                return;
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
