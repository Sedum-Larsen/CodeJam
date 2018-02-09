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
                var naomi = Console.ReadLine().Split(' ').Select(s => double.Parse(s)).ToList();
                var ken = Console.ReadLine().Split(' ').Select(s => double.Parse(s)).ToList();
                var war = PlayWar(naomi, ken);

                var dwar = PlayDWar(naomi, ken);

                Console.WriteLine($"Case #{tc}: {dwar} {war}");
            }
        }

        private static int PlayDWar(List<double> naomi, List<double> ken)
        {
            int result = 0;
            for(int offset = 0; offset < naomi.Count; offset++)
            {
                int current = 0;
                for(int ix = 0; ix < naomi.Count; ix++)
                {
                    if (ix+offset >= naomi.Count) break;
                    if (naomi[ix+offset] > ken[ix]) current++;
                }
                result = Math.Max(result, current);
            }
            return result;
        }

        private static int PlayWar(List<double> naomi, List<double> ken)
        {
            naomi.Sort();
            ken.Sort();
            var result = 0;
            var taken = new List<double>();
            foreach(var number in naomi)
            {
                var low = 1.0;
                var match = false;
                foreach(var knum in ken)
                {
                    if (!taken.Contains(knum))
                    {
                        if (knum > number)
                        {
                            match = true;
                            taken.Add(knum);
                            break;
                        }
                        low = Math.Min(low, knum);
                    }
                }
                if (!match)
                {
                    result++;
                    taken.Add(low);
                }
            }
            return result;
        }
    }
}