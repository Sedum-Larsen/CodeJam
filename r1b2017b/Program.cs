using System;
using System.Collections.Generic;
using System.Linq;

namespace r1b2017b
{
    class Program
    {
        private static char[] solution;
        private static HashSet<string> mem;
        private static int N;
        static void Main(string[] args)
        {
            //Console.SetIn(new System.IO.StreamReader("test.in"));
            var tmax = int.Parse(Console.ReadLine());
            for (int tc = 1; tc <= tmax; tc++)
            {
                //N, R, O, Y, G, B, and V -- O=RY, G=YB, V=BR
                var parts = Console.ReadLine().Split(' ');
                N = int.Parse(parts[0]);
                int R = int.Parse(parts[1]);
                int O = int.Parse(parts[2]);
                int Y = int.Parse(parts[3]);
                int G = int.Parse(parts[4]);
                int B = int.Parse(parts[5]);
                int V = int.Parse(parts[6]);
                solution = new char[N];
                solution[0] = '.';
                DoInit(R, O, Y, G, B, V);
                Console.WriteLine($"Case #{tc}: {(solution[0] != '.' ? new string(solution) : "IMPOSSIBLE")}");
            }

        }

        private static void DoInit(int r, int o, int y, int g, int b, int v)
        {
            //if (o > 2*b) return;
            //if (g > 2*r) return;
            //if (v > 2*y) return;
            //var btemp = b - 2*o;
            //var rtemp = r - 2*g;
            //var ytemp = y - 2*v;
            //if (btemp > (rtemp+ytemp)) return;
            //if (rtemp > (btemp+ytemp)) return;
            //if (ytemp > (rtemp+btemp)) return;

            State init = new State(r, o,y,g,b,v);
            char[] stalls = new char[N];
            if (r > 0)
            {
                mem = new HashSet<string>();
                stalls[0] = 'r';
                DoIt(init.DoAction('r'), stalls, 0);
                if (solution[0] != '.') return;
            }
            if (o > 0)
            {
                mem = new HashSet<string>();
                stalls[0] = 'o';
                DoIt(init.DoAction('o'), stalls,0);
                if (solution[0] != '.') return;
            }
            if (y > 0)
            {
                mem = new HashSet<string>();
                stalls[0] = 'y';
                DoIt(init.DoAction('y'), stalls,0);
                if (solution[0] != '.') return;
            }
            if (g > 0)
            {  
                mem = new HashSet<string>();
                stalls[0] = 'g';
                DoIt(init.DoAction('g'), stalls,0);
                if (solution[0] != '.') return;
            }
            if (b > 0)
            {
                mem = new HashSet<string>();
                stalls[0] = 'b';
                DoIt(init.DoAction('b'), stalls,0);
                if (solution[0] != '.') return;
            }
            if (v > 0)
            {
                mem = new HashSet<string>();
                stalls[0] = 'v';
                DoIt(init.DoAction('v'), stalls,0);
                if (solution[0] != '.') return;
            }
        }

        private static void DoIt(State state, char[] stalls, int turn)
        {
            if (turn == N-1)
            {
                if (State.IsValid(stalls[0], stalls[N-1]))
                {
                    solution = (char[])stalls.Clone();
                }
                return;
            }
            foreach(var unicorn in state.NextList(stalls[turn]))
            {
                var next = state.DoAction(unicorn);
                if (!mem.Contains(unicorn+next.ToString()))
                {
                    mem.Add(unicorn+next.ToString());
                    var newStalls = (char[])stalls.Clone();
                    newStalls[turn+1] = unicorn;
                    DoIt(next, newStalls, turn+1);
                    if (solution[0] != '.') return;
                }
            }
            
        }


        public class State
        {
            private static Dictionary<char, char[]> ValidList = new Dictionary<char, char[]>();

            public int R;
            public int O;
            public int Y;
            public int G;
            public int B;
            public int V;

            public State(int r, int o, int y, int g, int b, int v)
            {
                this.R = r;
                this.O = o;
                this.Y = y;
                this.G = g;
                this.B = b;
                this.V = v;
            }
            static State()
            {
                ValidList.Add('r', new char[] {'y', 'b', 'g'});
                ValidList.Add('o', new char[] {'b'});
                ValidList.Add('y', new char[] {'r', 'b', 'v'});
                ValidList.Add('g', new char[] {'r'});
                ValidList.Add('b', new char[] {'y', 'r', 'o'});
                ValidList.Add('v', new char[] {'y'});
            }
            public static bool IsValid(char v1, char v2)
            {
                if (ValidList[v1].Contains(v2)) return true;
                return false;
            }
            public List<char> NextList(char prev)
            {
                var x = ValidList[prev];
                bool inOrder = false;
                while(!inOrder)
                {
                    inOrder = true;
                    for(int ix = 0; ix < x.Length-1; ix++)
                    {
                        if (GetCount(x[ix]) < GetCount(x[ix+1]))
                        {
                            char temp = x[ix];
                            x[ix] = x[ix+1];
                            x[ix+1] = temp;
                            inOrder = false;
                        }
                    }
                }
                List<char> result = new List<char>();
                foreach(var c in x)
                    if (GetCount(c)>0) result.Add(c);
                return result;              

            }

            private int GetCount(char c)
            {
                switch (c)
                {
                    case 'r':
                        return R;
                    case 'o':
                        return O;
                    case 'y':
                        return Y;
                    case 'g':
                        return G;
                    case 'b':
                        return B;
                    case 'v':
                        return V;
                }
                throw new Exception("BAD");
            }

            public State DoAction(char unicorn)
            {
                State newState = new State(R, O, Y, G, B, V);
                switch (unicorn)
                {
                    case 'r':
                        newState.R--;
                        return newState;
                    case 'o':
                        newState.O--;
                        return newState;
                    case 'y':
                        newState.Y--;
                        return newState;
                    case 'g':
                        newState.G--;
                        return newState;
                    case 'b':
                        newState.B--;
                        return newState;
                    case 'v':
                        newState.V--;
                        return newState;
                }
                throw new Exception("WRONG");

            }

            public override string ToString()
            {
                return $"{R}#{O}#{Y}#{G}#{B}#{V}";
            }
        }
    }
}
