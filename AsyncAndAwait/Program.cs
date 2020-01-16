using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAndAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Hello World!");
            Display().Wait();
            Console.ReadKey();
        }

        static async Task Display()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {await GetInnerMessage()}");
        }

        static async Task<string> GetInnerMessage()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: getting inner message");

            return await Task.Run(() =>
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: getting inner message data");
                return "Inner Message Data";
            });

            //return task.Result;
        }

    }
}
