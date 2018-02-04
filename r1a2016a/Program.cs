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
                var parts = Console.ReadLine().ToCharArray();
                string result = new string(parts[0],1);
                for(int ix = 1 ; ix < parts.Length; ix++)
                {
                    if (parts[ix] >= result[0]) result = new string(parts[ix], 1) + result;
                    else result = result + new string(parts[ix], 1);
                }

                Console.WriteLine($"Case #{tc}: {result}");
            }
        }
    }
}


