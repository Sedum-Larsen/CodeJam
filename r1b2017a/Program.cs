using System;

namespace r1b2017a
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetIn(new System.IO.StreamReader("test.in"));
            var tmax = int.Parse(Console.ReadLine());
            for (int tc = 1; tc <= tmax; tc++)
            {
                var parts = Console.ReadLine().Split(' ');
                int D = int.Parse(parts[0]);
                int N = int.Parse(parts[1]);
                double slowest = 0d;
                for(int ix = 0; ix < N; ix++)
                {
                    parts = Console.ReadLine().Split(' ');
                    slowest = Math.Max(slowest, 1.0*(D-int.Parse(parts[0]))/int.Parse(parts[1]));
                }
                double speed = 1.0*D/slowest;
                Console.WriteLine($"Case #{tc}: {speed.ToString("#.000000").Replace(',','.')}");
            }
        }
    }
}
