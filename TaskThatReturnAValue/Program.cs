using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskThatReturnAValue
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            var employee = new Employee()
            {
                Name = "Abdelkhalek Amer",
                CompanyName = "Integrant Inc.",
                Salary = 100m
            };

            var bonusWorkerTask = new Task<decimal>(() => 25m);

            var bonusAddingWorkerTask = new Task(() =>
            {
                Thread.CurrentThread.Name = "Bonus adding worker thread";
                AddBonusRoEmployee(employee, bonusWorkerTask.Result);
            });

            // Deadlock occurs if start code is commented 
            // because task isn't started yet and the main thread is waiting for it
            bonusWorkerTask.Start();
            bonusAddingWorkerTask.Start();
            bonusAddingWorkerTask.Wait();
            Console.WriteLine($"{Thread.CurrentThread.Name}: Bonus is added.");

        }

        private static void AddBonusRoEmployee(Employee employee, decimal bonus)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {employee.Name} is paid {employee.Salary + bonus}$");
        }

    }

    internal class Employee
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public decimal Salary { get; set; }
    }
}
