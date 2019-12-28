using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinuation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var antecedent = Task.Run(() =>
            {
                return DateTime.UtcNow.Year.ToString();
            });

            Task<string> continuation = antecedent.ContinueWith(data =>
            {
                Thread.CurrentThread.Name = "worker thread";
                return $"{Thread.CurrentThread.Name}: Current year is {data.Result}";
            });

            Console.WriteLine($"{Thread.CurrentThread.Name} logs from {continuation.Result}");
        }
    }
}
