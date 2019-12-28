using System;
using System.Threading;

namespace MonitorAndLock
{
    internal class Account
    {
        private object lockObj = new object();
        private int balance = 0;
        private Random random = new Random();
        public Account(int initialBalanace)
        {
            balance = initialBalanace;
        }

        private int withdraw(int amount)
        {
            if (balance < 0)
            {
                throw new Exception("Not enough balance");
            }

            Monitor.Enter(lockObj);
            try
            {
                if (balance >= amount)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: amount withdrawn {amount}$");
                    balance -= amount;
                    return balance;
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
            return 0;
        }

        public void withdrawRandomly()
        {
            if (Thread.CurrentThread.Name != $"Worker thread {Thread.CurrentThread.ManagedThreadId}")
            {
                Thread.CurrentThread.Name = $"Worker thread {Thread.CurrentThread.ManagedThreadId}";
            }

            for (var i = 0; i < 5; i++)
            {
                var balance = withdraw(random.Next(2000, 5000));
                if (balance > 0)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}:Balance left {balance}$");
                }
                else if (balance == 0)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}:Balance is 0$");
                }
            }
        }

    }
}
