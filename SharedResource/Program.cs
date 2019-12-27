using System;
using System.Threading;

namespace SharedResource
{
    internal class Program
    {
        private static bool isCompleted = false;
        private static object lockObj = new object();
        private static void Main(string[] args)
        {
            var thread = new Thread(printIsCompletedMessage);
            thread.Name = "Worker thread";
            thread.Start();
            Thread.CurrentThread.Name = "Main thread";
            printIsCompletedMessage();

        }

        private static void printIsCompletedMessage()
        {
            lock (lockObj)
            {
                if (!isCompleted)
                {
                    isCompleted = true;
                    Console.WriteLine($"Code is completed by thread: {Thread.CurrentThread.Name}");
                }
            }
        }
    }
}
