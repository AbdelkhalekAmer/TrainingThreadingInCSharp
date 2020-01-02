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
            new Thread(AcquireMutex_TimedMutexAcuisition) { Name = "worker thread 1" }.Start();
            new Thread(AcquireMutex_TimedMutexAcuisition) { Name = "worker thread 2" }.Start();
            new Thread(AcquireMutex_TimedMutexAcuisition) { Name = "worker thread 3" }.Start();

        }

        static void AcquireMutex_TimedMutexAcuisition()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: trying to acquire mutex");
            if (mutex.WaitOne(1000, false))
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: protected code by mutex");
                Thread.Sleep(2000);
                mutex.ReleaseMutex();
                Console.WriteLine($"{Thread.CurrentThread.Name}: mutex is released");
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: couldn't acquire mutex");
            }
        }

        static void AcquuireMutex()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: trying to acquire mutex");
            if (mutex.WaitOne())
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: protected code by mutex");
                mutex.ReleaseMutex();
                Console.WriteLine($"{Thread.CurrentThread.Name}: mutex is released");
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: couldn't acquire mutex");
            }

        }

    }
}
