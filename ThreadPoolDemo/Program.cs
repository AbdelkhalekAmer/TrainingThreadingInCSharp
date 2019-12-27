using System;
using System.Threading;

namespace ThreadPoolDemo
{
    internal class Program
    {
        private static int count = 0;
        private static object lockObject = new object();
        private static void Main(string[] args)
        {
            
            Thread.CurrentThread.Name = "Main thread";
            var employee = new Employee()
            {
                Name = "Abdelkhalek Amer",
                CompanyName = "Integrant Inc."
            };

            new Thread(() =>
            {
                printEmployeeInfo(employee);
            }).Start();

            ThreadPool.QueueUserWorkItem(new WaitCallback(printEmployeeInfo), employee);

            printEmployeeInfo(employee);

        }
        private static void printEmployeeInfo(object input)
        {
            if (Thread.CurrentThread.Name != "Main thread")
            {
                var isThreadPoolThreadMessage = Thread.CurrentThread.IsThreadPoolThread ? "(in thread pool)" :
                    "(not in thread pool)";
                lock (lockObject)
                {
                    Thread.CurrentThread.Name = $"Worker{++count} thread {isThreadPoolThreadMessage}";
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
