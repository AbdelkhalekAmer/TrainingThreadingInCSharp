using System;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var thread = new Thread(() =>
                {
                    try
                    {
                        for (var i = 0; i < 50; i++)
                        {
                            Console.WriteLine($"Worker thread counts: {i}");
                        }
                        throw new Exception("Exception was thrown in worker thread.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });

                thread.Start();

                for (var i = 0; i < 50; i++)
                {
                    Console.WriteLine($"Main thread counts: {i}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
