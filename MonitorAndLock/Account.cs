using System;
using System.Threading;

namespace MonitorAndLock
{
    internal class Account
    {
        private object lockObj = new object();
        private int balance = 0;
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
            var ba
        }

    }
}
