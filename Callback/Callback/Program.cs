using System;
using System.Collections.Generic;
using System.Linq;

namespace Callback
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = new int[] { -2, 6, 8 };
            Console.WriteLine(ints.MyAny(i => i % 2 == 0 && i < 0));
            Console.WriteLine(ints.MyAll(i => i % 2 == 0));
        }
    }
}
