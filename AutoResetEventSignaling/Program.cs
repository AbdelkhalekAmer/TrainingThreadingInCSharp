using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventSignaling
{
    internal class Program
    {
        private static EventWaitHandle autoResetEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        private static int count = 0;
        private static void Main(string[] args)
        {
            autoResetEvent.Set();
            Task.WaitAll(Task.Factory.StartNew(AccessCode),
                         Task.Factory.StartNew(AccessCode),
                         Task.Factory.StartNew(AccessCode),
                         Task.Factory.StartNew(AccessCode),
                         Task.Factory.StartNew(AccessCode),
                         Task.Factory.StartNew(AccessCode),
                         Task.Factory.StartNew(AccessCode));
        }

        private static void AccessCode()
        {
            Console.WriteLine($"Task {Task.CurrentId}: try to access code");

            autoResetEvent.WaitOne();

            count++;
            Console.WriteLine($"Task {Task.CurrentId}: accessed a protected code");
            Console.WriteLine($"Task {Task.CurrentId}: finished the protected code");
            // if you commented the following if block, Deadlock occurs because all the waiting threads will not
            // move on to complete the code and the main thread will also wait them to finish as Task.WaitAll() is used.
            if (count >= 1)
            {
                autoResetEvent.Set();
            }
        }
    }
}
