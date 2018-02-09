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
            var work = new Queue<State>();
            var best = 0;
            var memory = new HashSet<string>();
            work.Enqueue(new State(0, new List<double>(naomi), new List<double>(ken)));
            while(work.Count > 0)
            {
                var next = work.Dequeue();
                best = Math.Max(best, next.Wins);
                if (next.Naomi.Count == 0) continue;
                if (next.Wins + next.Naomi.Count < best) continue;
                var iyy = next.KensMove(next.LastValueKen());
                var chosenKenHigh = next.Ken[iyy];
                for(int ix = 0; ix < next.Naomi.Count; ix++)
                {
                    if (ix == 0 || (ix == next.Naomi.Count -1))
                    {
                        var chosenNaomi = next.Naomi[ix];
                        var iy = next.KensMove(chosenNaomi);
                        var chosenKen = next.Ken[iy];
                        work.Enqueue(new State(next.NextWin(chosenNaomi, chosenKen), next.nextNaomi(ix), next.nextKen(iy)));
                        work.Enqueue(new State(next.NextWin(chosenNaomi, chosenKenHigh), next.nextNaomi(ix), next.nextKen(iyy)));
                    }
                }
            }
            return best;
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

    class State
    {
        public int Wins { get; set; }
        public List<double> Naomi { get; set; }
        public List<double> Ken { get; set; }

        public State(int wins, List<double> naomi, List<double> ken)
        {
            this.Wins = wins;
            this.Naomi = naomi;
            this.Ken = ken;
        }

        public int KensMove(double chosen)
        {
            double low = 1.0;
            for(int ix = 0; ix < Ken.Count; ix++)
            {
                if (Ken[ix]>chosen) return ix;
                low = Math.Min(low, Ken[ix]);
            }
            return Ken.IndexOf(low);
        }

        public List<double> nextNaomi(int ix)
        {
            var result = new List<double>(Naomi);
            result.RemoveAt(ix);
            return result;
        }

        public List<double> nextKen(int ix)
        {
            var result = new List<double>(Ken);
            result.RemoveAt(ix);
            return result;
        }

        public int NextWin(double chosenNaomi, double chosenKen)
        {
            return Wins + (chosenNaomi > chosenKen ? 1 : 0);
        }

        public double LastValueKen()
        {
            return Ken[Ken.Count-1] - 0.0001;
        }

        public string stateString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var num in Naomi) sb.Append($"{num}#");
            foreach(var num in Ken) sb.Append($"{num}#");
            return sb.ToString();
        }
    }
}