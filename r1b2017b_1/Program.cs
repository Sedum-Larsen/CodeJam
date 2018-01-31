using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r1b2017b
{
    class Program
    {
        private static Dictionary<char, char[]> ValidList = new Dictionary<char, char[]>();
        static void Main(string[] args)
        {
            //Console.SetIn(new System.IO.StreamReader("test.in"));
            var tmax = int.Parse(Console.ReadLine());
            ValidList.Add('r', new char[] {'y', 'b', 'g'});
            ValidList.Add('o', new char[] {'b'});
            ValidList.Add('y', new char[] {'r', 'b', 'v'});
            ValidList.Add('g', new char[] {'r'});
            ValidList.Add('b', new char[] {'y', 'r', 'o'});
            ValidList.Add('v', new char[] {'y'});
            for (int tc = 1; tc <= tmax; tc++)
            {
                //N, R, O, Y, G, B, and V -- O=RY, G=YB, V=BR
                var parts = Console.ReadLine().Split(' ');
                int N = int.Parse(parts[0]);
                int R = int.Parse(parts[1]);
                int O = int.Parse(parts[2]);
                int Y = int.Parse(parts[3]);
                int G = int.Parse(parts[4]);
                int B = int.Parse(parts[5]);
                int V = int.Parse(parts[6]);
                
                
                
                Console.WriteLine($"Case #{tc}: {DoIt(N, R, O, Y, G, B, V)}");
            }

        }

        private static string DoIt(int n, int r, int o, int y, int g, int b, int v)
        {
            n -= 2*o;
            b -= o;
            n -= 2*g;
            r -= g;
            n -= 2*v;
            y -= v;
            
            if (b < 0 || r < 0 || y < 0) return "IMPOSSIBLE";
            if (b > (r+y)) return "IMPOSSIBLE";
            if (r > (b+y)) return "IMPOSSIBLE";
            if (y > (r+b)) return "IMPOSSIBLE";
            
            string b_append = string.Concat(Enumerable.Repeat("ob", o));
            string r_append = string.Concat(Enumerable.Repeat("gr", g));
            string y_append = string.Concat(Enumerable.Repeat("vy", v));

            char[] list = new char[3];
            var max = Math.Max(b, Math.Max(y,r));
            if (b == max) list[0] = 'b';
            else if (y == max) list[0] = 'y';
            else list[0] = 'r';
            if (list[0] == 'b')
            {
                if (y > r)
                {
                    list[1] = 'y';
                    list[2] = 'r';
                }
                else
                {
                    list[1] = 'r';
                    list[2] = 'y';                    
                }
            }
            if (list[0] == 'y')
            {
                if (b > r)
                {
                    list[1] = 'b';
                    list[2] = 'r';
                }
                else
                {
                    list[1] = 'r';
                    list[2] = 'b';                    
                }
            }
            if (list[0] == 'r')
            {
                if (y > b)
                {
                    list[1] = 'y';
                    list[2] = 'b';
                }
                else
                {
                    list[1] = 'b';
                    list[2] = 'y';                    
                }
            }
            var iy = 0;
            string[] solution = new string[n];
            var ix = 0;
            while (ix < n)
            {
                char next = list[iy];
                var count = 0;
                switch (next)
                {
                    case 'b':
                        count = b;
                        break;
                    case 'y':
                        count = y;
                        break;
                    case 'r':
                        count = r;
                        break;
                }
                if (count == 0)
                {
                    iy++;
                }
                else
                {
                    solution[ix] = next.ToString();
                    switch (next)
                    {
                        case 'b':
                            b--;
                            solution[ix] += b_append;
                            b_append = "";
                            break;
                        case 'y':
                            y--;
                            solution[ix] += y_append;
                            y_append = "";
                            break;
                        case 'r':
                            r--;
                            solution[ix] += r_append;
                            r_append = "";
                            break;
                    }
                    ix += 2;
                }
            }
            ix = 1;
            while (ix < n)
            {
                char next = list[iy];
                var count = 0;
                switch (next)
                {
                    case 'b':
                        count = b;
                        break;
                    case 'y':
                        count = y;
                        break;
                    case 'r':
                        count = r;
                        break;
                }
                if (count == 0)
                {
                    iy++;
                }
                else
                {
                    solution[ix] = next.ToString();
                    switch (next)
                    {
                        case 'b':
                            b--;
                            solution[ix] += b_append;
                            b_append = "";
                            break;
                        case 'y':
                            y--;
                            solution[ix] += y_append;
                            y_append = "";
                            break;
                        case 'r':
                            r--;
                            solution[ix] += r_append;
                            r_append = "";
                            break;
                    }
                    ix += 2;
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach(var s in solution) sb.Append(s);
            var result = sb.ToString()+b_append+y_append+r_append;
            char[] c_res = result.ToCharArray();
            for(int iz = 0; iz < c_res.Length-1; iz++)
                if (!ValidList[c_res[iz]].Contains(c_res[iz+1])) return "IMPOSSIBLE";
            if (!ValidList[c_res[0]].Contains(c_res[c_res.Length-1])) return "IMPOSSIBLE";
            return result;
        }
    }
}
