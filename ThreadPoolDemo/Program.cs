using System;
using System.Linq;
using System.Threading;

namespace ThreadPoolDemo
{
    internal class Program
    {
        private static int count = 0;
        private static string[] threadNames = new[] { "Main thread", "External thread" };
        private static object lockObject = new object();
        private static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            ThreadPool.GetMinThreads(out var minThreads, out var minPortThreads);
            ThreadPool.SetMaxThreads(minPortThreads * 2, minPortThreads * 2);

            var employee = new Employee()
            {
                Name = "Abdelkhalek Amer",
                CompanyName = "Integrant Inc."
            };

            // Main thread 
            printEmployeeInfo(employee);

            // Worker thread
            new Thread(() =>
            {
                printEmployeeInfo(employee);
            })
            {
                Name = "External thread"
            }.Start();

            // worker threads in thread pool
            for (var i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(printEmployeeInfo), employee);
            }
        }
        private static void printEmployeeInfo(object input)
        {

            if (!threadNames.Any(name => name == Thread.CurrentThread.Name))
            {
                var isThreadPoolThreadMessage = Thread.CurrentThread.IsThreadPoolThread ? "(in thread pool)" :
                    "(not in thread pool)";
                lock (lockObject)
                {
                    Thread.CurrentThread.Name = $"Worker {++count} thread {isThreadPoolThreadMessage}";
                }
            }
            var employee = input as Employee;
            if (employee != null)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {employee.Name} is working in {employee.CompanyName}");
            }
            else
            {
                Console.WriteLine("Invalid employee data.");
            }
        }
    }
    internal class Employee
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}
