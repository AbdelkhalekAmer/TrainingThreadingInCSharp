using System;
using System.Threading;

namespace LocalAndHeapMemory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var array = new[] { 1, 2, 3 };

            int num = 3;

            new Thread(() => printArray(array))
            {
                Name = "Worker thread"
            }.Start();

            new Thread(() => printNumber(num))
            {
                Name = "Worker2 thread"
            }.Start();

            printArray(array);
            printNumber(num);
        }

        private static void printArray(int[] arr)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {++arr[0]} - {++arr[1]} - {++arr[2]}");
        }

        private static void printNumber(int x)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {++x}");
        }

    }
}
