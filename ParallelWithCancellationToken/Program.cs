using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelWithCancellationToken
{
    class Program
    {
        static object lockObj = new object();
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            //===================================================================================
            // prepare data
            var numsList = Enumerable.Range(0, 10).ToArray();

            // prepare parallel options and cancellation token
            var cancellationTokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            //===================================================================================

            Console.WriteLine("Press X to cancel...");

            Task.Factory.StartNew(() =>
            {

                if (Console.ReadKey().KeyChar == 'x')
                {
                    cancellationTokenSource.Cancel();
                }

            });

            long total = 0;

            try
            {
                Parallel.For<long>(0, numsList.Length, parallelOptions, () => 0, (count, parallelLoopState, subTotal) =>
                {
                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                    subTotal += numsList[count];
                    Console.WriteLine($"Worker thread {Thread.CurrentThread.ManagedThreadId}: subtotal is {subTotal}");

                    return subTotal;
                },
                (subTotal) =>
                {
                    Interlocked.Add(ref total, subTotal);
                    Console.WriteLine($"Worker thread {Thread.CurrentThread.ManagedThreadId}: total for now is {total}");
                    //lock (lockObj)
                    //{
                    //    total += subTotal;
                    //    Console.WriteLine($"Worker thread {Thread.CurrentThread.ManagedThreadId}: total for now is {total}");
                    //}
                    //total += subTotal;
                    //Console.WriteLine($"Worker thread {Thread.CurrentThread.ManagedThreadId}: total for now is {total}");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Worker thread {Thread.CurrentThread.ManagedThreadId}: operations is canceled and exception is thrown {{{ex.Message}}}");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{Thread.CurrentThread.Name}: total is {total}");

        }
    }
}
