using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeJamTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetIn(new System.IO.StreamReader("test.in"));
            var tmax = int.Parse(Console.ReadLine());
            for(int tc = 1; tc <= tmax; tc++)
            {
                var parts = Console.ReadLine().Split(' ').Select(s => double.Parse(s)).ToArray();
                var c = parts[0];
                var f = parts[1];
                var x = parts[2];

                var best = x / 2.0;
                var elapsed = 0d;
                var rate = 2.0;
                while (elapsed < best)
                {
                    best = Math.Min(best, elapsed + x / rate);
                    elapsed += c/rate;
                    rate += f;
                }
                
                

                Console.WriteLine($"Case #{tc}: {best.ToString("0.#######").Replace(',','.')}");
            }
        }
    }
}


