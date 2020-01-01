using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLock
{
    class Program
    {
        static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        static Dictionary<int, string> persons = new Dictionary<int, string>();
        static Random random = new Random();
        static int count = 0;
        static object countLock = new object();
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            var task1 = Task.Factory.StartNew(Read);
            var task2 = Task.Factory.StartNew(Write, "User A");
            var task3 = Task.Factory.StartNew(Read);
            var task4 = Task.Factory.StartNew(Write, "User B");
            var task5 = Task.Factory.StartNew(Read);
            Task.WaitAll(task1, task2, task3, task4, task5);
            Console.WriteLine($"{Thread.CurrentThread.Name}: All tasks are finished and total persons = {persons.Count}");
        }

        static void Read()
        {
            lock (countLock)
            {
                Thread.CurrentThread.Name = $"Worker thread {++count}";
            }
            Console.WriteLine($"{Thread.CurrentThread.Name}: started reading");
            for (int i = 0; i < 10; i++)
            {
                readerWriterLockSlim.EnterReadLock();
                Thread.Sleep(50);
                readerWriterLockSlim.ExitReadLock();
            }
            Console.WriteLine($"{Thread.CurrentThread.Name}: finished reading");
        }

        static void Write(object userName)
        {
            lock (countLock)
            {
                Thread.CurrentThread.Name = $"Worker thread {++count}";
            }
            for (int i = 0; i < 10; i++)
            {
                readerWriterLockSlim.EnterWriteLock();
                var id = random.Next(2000, 5000);
                persons.Add(id, $"Person-{id}");
                Console.WriteLine($"{Thread.CurrentThread.Name}: {userName} added {persons[id]}");
                readerWriterLockSlim.ExitWriteLock();
            }
        }

    }
}
