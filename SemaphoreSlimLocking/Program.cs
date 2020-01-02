using System;
using System.Threading;

namespace SemaphoreSlimLocking
{
    class Program
    {
        // a question here >>> why this code always have n - 1 threads
        // that complete acquire function
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(4);
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            new Thread(acquireCodeThroughSemaphore) { Name = "worker thread 1" }.Start();
            new Thread(acquireCodeThroughSemaphore) { Name = "worker thread 2" }.Start();
            new Thread(acquireCodeThroughSemaphore) { Name = "worker thread 3" }.Start();
            new Thread(acquireCodeThroughSemaphore) { Name = "worker thread 4" }.Start();
            new Thread(acquireCodeThroughSemaphore) { Name = "worker thread 5" }.Start();
            new Thread(acquireCodeThroughSemaphore) { Name = "worker thread 6" }.Start();
        }

        static void acquireCodeThroughSemaphore()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: is trying to acquire code");
            semaphoreSlim.Wait();
            if (semaphoreSlim.CurrentCount > 0)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: acquired a protected code");
                //Thread.Sleep(1000);
                Console.WriteLine($"{Thread.CurrentThread.Name}: the protected code is finished");
            }
            else
            {
                semaphoreSlim.Release();
            }
        }
    }
}
