using System;
using System.Threading;

namespace MutexLocking
{
    class Program
    {
        static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            new Thread(AcquuireMutex) { Name = "worker thread 1" }.Start();
            new Thread(AcquuireMutex) { Name = "worker thread 2" }.Start();
            new Thread(AcquuireMutex) { Name = "worker thread 3" }.Start();

        }

        static void AcquuireMutex()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: trying to acquire mutex");
            mutex.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name}: protected code by mutex");
            mutex.ReleaseMutex();
            Console.WriteLine($"{Thread.CurrentThread.Name}: mutex is released");
        }

    }
}
