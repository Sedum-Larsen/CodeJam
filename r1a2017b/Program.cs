using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r1a2017b
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetIn(new System.IO.StreamReader("test.in"));
            var tmax = int.Parse(Console.ReadLine());
            for(int tc = 1; tc<=tmax;tc++)
            {
                var parts = Console.ReadLine().Split(' ');
                int N = int.Parse(parts[0]);
                int P = int.Parse(parts[1]);
                int[] R = new int[N];
                int[][] Q = new int[N][];
                int[][] Min = new int[N][];
                int[][] Max = new int[N][];
                R = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
                for(int ix = 0; ix < N; ix++)
                {
                    Q[ix] = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
                    Min[ix] = new int[P];
                    Max[ix] = new int[P];
                    for(int iy = 0; iy < P; iy++)
                    {
                        int gns = Q[ix][iy] / R[ix];
                        int p_min = 0;
                        int p_max = 0;
                        var find = gns;
                        while(IsValid(Q[ix][iy], R[ix], find) && find > 0)
                        {
                            p_min = find;
                            find--;
                        }
                        find = gns+1;
                        while(IsValid(Q[ix][iy], R[ix], find))
                        {
                            p_max = find;
                            find++;
                        }
                        Min[ix][iy] = (p_min==0?p_max:p_min);
                        Max[ix][iy] = (p_max==0?p_min:p_max);
                    }
                }
                
                int min_kits = P;
                if (N == 1)
                {
                    min_kits = 0;
                    for(int ix = 0; ix < P; ix++)
                     if(Min[0][ix] != 0 && Max[0][ix] != 0) min_kits++;
                }
                for(int ix = 0; ix < N; ix++)
                {
                    for(int ixc = 0; ixc < N; ixc++)
                    {
                        if (ix==ixc) continue;
                        int paths = 0;
                        HashSet<int> ends = new HashSet<int>();
                        for(int iy = 0; iy<P; iy++)
                        {
                            if (Min[ix][iy] == 0 && Max[ix][iy] == 0) continue;
                            bool ok = false;
                            for(int iy2 = 0; iy2<P;iy2++)
                            {
                                if (Min[ix][iy] > Max[ixc][iy2] || Max[ix][iy] < Min[ixc][iy2]) continue;
                                ok = true;
                                if (!ends.Contains(iy2)) ends.Add(iy2);
                            }
                            if (ok) paths++;
                        }
                        paths = Math.Min(paths, ends.Count());
                        min_kits = Math.Min(min_kits,paths);
                    }
                }

                Console.WriteLine($"Case #{tc}: {min_kits}");
                /* 
                for(int ix = 0; ix < N; ix++)
                {
                    StringBuilder sb = new StringBuilder();
                    for(int iy = 0; iy < P; iy++)
                        sb.Append((iy==0?"":", ") + $"[{Min[ix][iy]};{Max[ix][iy]}]");
                    Console.WriteLine(sb.ToString());
                }
                */
            }
        }

        private static bool IsValid(int q, int r, int x)
        {
            if (r*x * 0.9 > q) return false;
            if (r*x * 1.1 < q) return false;
            return true;
        }
    }
}
