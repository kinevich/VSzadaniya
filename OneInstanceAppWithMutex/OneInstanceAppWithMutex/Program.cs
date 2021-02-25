using System;
using System.Threading;

namespace OneInstanceAppWithMutex
{
    class Program
    {
        static void Main(string[] args)
        {
            bool createdNew;

            new Mutex(true, "Mutex", out createdNew);

            if (!createdNew)
            {
                Console.WriteLine("App is already running! Exiting the application.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Continuing with the application");
            Console.ReadKey();
        }
    }
}
