using System;

namespace r1a2017a
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
                var r = int.Parse(parts[0]);
                var c = int.Parse(parts[1]);
                char[][] cake = new char[r][];
                for(int ix = 0; ix < r; ix++)
                    cake[ix] = Console.ReadLine().ToCharArray();
                for(int ix = 0; ix < r; ix++)
                {
                    char prev = '?';
                    for(int iy = 0; iy < c; iy++)
                    {
                        if (cake[ix][iy] == '?')
                        {
                            if (prev != '?') cake[ix][iy] = prev;
                        }
                        else
                        {
                            prev = cake[ix][iy];
                        }
                    }
                }
                for(int ix = 0; ix < r; ix++)
                {
                    char prev = '?';
                    for(int iy = c-1; iy > -1; iy--)
                    {
                        if (cake[ix][iy] == '?')
                        {
                            if (prev != '?') cake[ix][iy] = prev;
                        }
                        else
                        {
                            prev = cake[ix][iy];
                        }
                    }
                }
                for(int ix = 0; ix < r-1; ix++)
                    if (cake[ix][0] != '?' && cake[ix+1][0] == '?') cake[ix+1] = cake[ix];
                for(int ix = r-1; ix > 0; ix--)
                    if (cake[ix][0] != '?' && cake[ix-1][0] == '?') cake[ix-1] = cake[ix];
                Console.WriteLine($"Case #{tc}:");
                for(int ix = 0; ix < r; ix++)
                    Console.WriteLine(new string(cake[ix]));
            }
        }
    }
}
