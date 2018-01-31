using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace graph
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetIn(new System.IO.StreamReader("b.in"));
            var Tmax = int.Parse(Console.ReadLine());
            for(int tc = 1  ; tc <= Tmax; tc++)
            {
                var parts = Console.ReadLine().Split(' ');
                var N = int.Parse(parts[0]);
                var Q = int.Parse(parts[1]);
                int[] E = new int[N];
                int[] S = new int[N];
                for(int ix = 0; ix < N; ix++)
                {
                    parts = Console.ReadLine().Split(' ');
                    E[ix] = int.Parse(parts[0]);
                    S[ix] = int.Parse(parts[1]);
                }
                Dictionary<int, List<int>> Map = new Dictionary<int, List<int>>();
                int[][] Distances = new int[N][];
                for(int ix = 0; ix < N; ix++)
                {
                    List<int> valid = new List<int>();
                    Distances[ix] = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
                    for(int iy = 0; iy < N; iy++)
                        if (Distances[ix][iy] != -1) valid.Add(iy);
                    Map.Add(ix, valid);
                }
                int[] U = new int[Q];
                int[] V = new int[Q];
                for(int ix = 0; ix < Q; ix++)
                {
                    parts = Console.ReadLine().Split(' ');
                    U[ix] = int.Parse(parts[0]) - 1;
                    V[ix] = int.Parse(parts[1]) - 1;
                }
                List<double> result = new List<double>();
                for(int ix = 0; ix < Q; ix++)
                {
                    var initState = new State(U[ix], 0,0,0d, new List<int>());
                    Queue<State> work = new Queue<State>();
                    work.Enqueue(initState);
                    double bestTime = double.MaxValue;
                    Dictionary<string, double> mem = new Dictionary<string, double>();
                    while(work.Count > 0)
                    {
                        var next = work.Dequeue();
                        //Console.WriteLine(next.Start + " " + next.ToString() + " " + next.Time.ToString());
                        if (next.Start == V[ix])
                        {
                            bestTime = Math.Min(bestTime, next.Time);
                        }
                        if (next.Time > bestTime) continue;
                        List<Horse> choices = new List<Horse>();
                        if (next.RestDist > 0) choices.Add(new Horse() { Dist = next.RestDist, Speed = next.Speed});
                        if (!next.Path.Contains(next.Start)) choices.Add(new Horse() { Dist = E[next.Start], Speed = S[next.Start]});
                        if (choices.Count != 0)
                        {
                            choices.Sort();
                            foreach (var item in Map[next.Start])
                            {
                                //var found=0;
                                foreach (var choice in choices)
                                {
                                    if (Distances[next.Start][item] <= choice.Dist /*&& found<2 */)
                                    {
                                        List<int> newPath = new List<int>();
                                        newPath.AddRange(next.Path);
                                        newPath.Add(next.Start);
                                        var NewState = new State(item, choice.Dist-Distances[next.Start][item], choice.Speed, next.Time + (1.0 * Distances[next.Start][item] / choice.Speed), newPath);
                                        if (!mem.Keys.Contains(NewState.ToString()) || mem[NewState.ToString()] > NewState.Time)
                                        {
                                            work.Enqueue(NewState);
                                            if (!mem.Keys.Contains(NewState.ToString())) mem.Add(NewState.ToString(), 0d);
                                            mem[NewState.ToString()] = NewState.Time;
                                            //found++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    result.Add(bestTime);
                }
                StringBuilder sb = new StringBuilder();
                foreach (var item in result)
                {
                    if (item != double.MaxValue) sb.Append($" {item.ToString("0.######").Replace(',','.')}");
                }
                Console.WriteLine($"Case #{tc}:{sb.ToString()}");
            }
        }

        public class Horse : IComparable<Horse>
        {
            public int Dist { get; set; }
            public int Speed { get; set; }

            public int CompareTo(Horse obj)
            {
                return obj.Speed.CompareTo(Speed);
            }
        }

        public class State
        {
            public int Start { get; private set; }
            public int RestDist { get; private set; }
            public int Speed { get; private set; }
            public double Time { get; private set; }
            public List<int> Path {get; private set;}

            public State(int start, int restDist, int speed, double time, List<int> path)
            {
                this.Start = start;
                this.RestDist = restDist;
                this.Speed = speed;
                this.Time = time;
                this.Path = path;
            }

            public override string ToString()
            {   
                StringBuilder sb = new StringBuilder();
                foreach(var p in Path) sb.Append($"{p}#");
                return $"{Start}-{RestDist}-{Speed}{sb.ToString()}";
            }
        }
    }
}