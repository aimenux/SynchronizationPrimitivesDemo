using System;

namespace App.Resource
{
    public interface IPrinter
    {
        string Id { get; }
        State State { get; }
        void Print(ConsoleColor color, string message);
    }
}