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
                return;
            }

            Console.WriteLine("Continuing with the application");
            Console.ReadKey();
        }
    }
}
