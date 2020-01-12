using System;
using System.Threading.Tasks;

namespace AttachedAndDetachedTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"{Task.CurrentId}: started");
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"{Task.CurrentId}: is calling...");
                }, TaskCreationOptions.AttachedToParent);
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"{Task.CurrentId}: is calling...");
                }, TaskCreationOptions.AttachedToParent);
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"{Task.CurrentId}: is calling...");
                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine($"{Task.CurrentId}: completed");
            });
            task.Wait();
            Console.WriteLine("All finished.");

        }
    }
}
