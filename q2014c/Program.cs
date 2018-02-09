using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeJamTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new System.IO.StreamReader("test.in"));
            var tmax = int.Parse(Console.ReadLine());
            for(int tc = 1; tc <= tmax; tc++)
            {
                var ints = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
                var r = ints[0];
                var c = ints[1];
                var m = ints[2];
                

                

                Console.WriteLine($"Case #{tc}:");
            }
        }
    }
}


