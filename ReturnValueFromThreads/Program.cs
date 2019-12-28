using System;
using System.Threading;

namespace ReturnValueFromThreads
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var employee = new Employee()
            {
                Name = "Abdelkhalek Amer",
                CompanyName = "Integrant Inc.",
                Salary = 100m
            };

            var bonus = 0m;

            var bonusWorkerThread = new Thread(() =>
            {
                bonus += 25m;
            })
            {
                Name = "Bonus worker thread"
            };

            bonusWorkerThread.Start();
            bonusWorkerThread.Join();

            var bonusAddingWorkerThread = new Thread(() =>
            {
                AddBonusRoEmployee(employee, bonus);
            })
            {
                Name = "Bonus adding worker thread"
            };

            bonusAddingWorkerThread.Start();
            bonusAddingWorkerThread.Join();

            Console.WriteLine("Bonus is added.");

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
