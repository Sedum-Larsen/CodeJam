using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
                var n = int.Parse(Console.ReadLine());
                Dictionary<int, int> numbers = new Dictionary<int, int>();
                for(int ix = 0; ix < 2*n-1; ix++)
                {
                    var ints = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
                    foreach(var num in ints)
                    {
                        if (!numbers.ContainsKey(num)) numbers.Add(num,0);
                        numbers[num]++;
                    }
                }
                var res = numbers.Where(r => r.Value % 2 == 1).Select(r => r.Key).ToList();
                res.Sort();
                StringBuilder sb = new StringBuilder();
                for(int ix = 0; ix < res.Count; ix++)
                    sb.Append((ix > 0 ? " " : "") + res[ix]);
                

                Console.WriteLine($"Case #{tc}: {sb.ToString()}");
            }
        }
    }
}


