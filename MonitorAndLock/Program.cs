using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorAndLock
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var account = new Account(20000);

            Task task1 = Task.Factory.StartNew(() =>
            {
                account.withdrawRandomly();
            });

            Task task2 = Task.Factory.StartNew(() =>
            {
                account.withdrawRandomly();
            });

            Task task3 = Task.Factory.StartNew(() =>
            {
                account.withdrawRandomly();
            });

            Task.WaitAll(task1, task2, task3);

            Console.WriteLine($"{Thread.CurrentThread.Name}: All Tasks are done");

        }
    }
}
