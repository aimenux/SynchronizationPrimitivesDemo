using System;
using System.Threading;

namespace SynchronizationPrimitivesDemo
{
    public static class Extensions
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public static string GetThreadName(this Thread thread)
        {
            return thread.Name == null
                ? $"{thread.ManagedThreadId:0000}"
                : $"{thread.ManagedThreadId:0000}@{thread.Name}";
        }

        public static void WriteLine(this ConsoleColor color, object value)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static ConsoleColor RandomColor()
        {
            return RandomEnumValue<ConsoleColor>();
        }

        private static T RandomEnumValue<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var index = Random.Next(2, values.Length);
            return (T) values.GetValue(index);
        }
    }
}
