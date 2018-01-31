using System;
using System.Collections.Generic;

namespace r1a2017c
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
                var Hd = int.Parse(parts[0]);
                var Ad = int.Parse(parts[1]);
                var Hk = int.Parse(parts[2]);
                var Ak = int.Parse(parts[3]);
                var B  = int.Parse(parts[4]);
                var D  = int.Parse(parts[5]);
                //var min_turns = DoRecursive(Hd, Ad, Hk, Ak, B, D);
                var InitState = new State(Hd, Ad, Hk, Ak, B, D);
                var min_turns = DoIt(InitState);
                Console.WriteLine($"Case #{tc}: {(min_turns == int.MaxValue ? "IMPOSSIBLE":min_turns.ToString())}");
            }

        }

        private static int DoIt(State initState)
        {
            HashSet<string> mem = new HashSet<string>();
            Queue<State> work = new Queue<State>();
            work.Enqueue(initState);
            mem.Add(initState.ToString());
            while(work.Count != 0)
            {
                State s = work.Dequeue();
                //Console.WriteLine(s.Turns.ToString() + ": " + s.ToString() + " -- " + work.Count.ToString());
                if (s.Hd <= 0) continue;
                if (s.Hk <= s.Ad) return s.Turns+1;
                if (s.Hd > s.Ak)
                {
                    var nextAttack = new State(s, 'a');
                    Validate(mem, work, nextAttack);
                    if (s.B > 0)
                    {
                        var nextBuff = new State(s, 'b');
                        Validate(mem, work, nextBuff);
                    }
                }
                var nextCure = new State(s, 'c');
                Validate(mem, work, nextCure);
                if (s.D > 0 && s.Ak > 0)
                {
                    var nextDebuff = new State(s, 'd');
                    Validate(mem, work, nextDebuff);
                }
            }
            return int.MaxValue;
        }

        private static void Validate(HashSet<string> mem, Queue<State> work, State next)
        {
            if (!mem.Contains(next.ToString()))
            {
                work.Enqueue(next);
                mem.Add(next.ToString());
            }
        }
        /*
private static int DoRecursive(int hd, int ad, int hk, int ak, int B, int D)
{
var mem = new HashSet<string>();
Queue<State> work = new Queue<State>();
var hd_c = hd;
var init = new State() {Hd = hd, Ad=ad, Hk=hk, Ak=ak, Turns = 0};
work.Enqueue(init);
while(work.Count>0)
{
var next = work.Dequeue();
if (next.Hk - next.Ad <= 0)  //Kill
{
  return next.Turns+1;
}

if (next.Hd > next.Ak)
{
  //Attack
  var move = new State() {Hd = next.Hd - next.Ak, Ad = next.Ad, Hk = next.Hk-next.Ad, Ak=next.Ak, Turns = next.Turns+1};
  string check = GetCheck(move);
  if (!mem.Contains(check))
  {
      mem.Add(check);
      work.Enqueue(move);
  }
  //Bluff
  if (B > 0) 
  {
      var move2 = new State() {Hd = next.Hd - next.Ak, Ad = next.Ad+B, Hk = next.Hk, Ak=next.Ak, Turns = next.Turns+1};
      string check2 = GetCheck(move2);
      if (!mem.Contains(check2))
      {
          mem.Add(check2);
          work.Enqueue(move2);
      }
  }
}
//Cure 
var move3 = new State() {Hd = hd_c - next.Ak, Ad = next.Ad, Hk = next.Hk, Ak=next.Ak, Turns = next.Turns+1};
var check3 = GetCheck(move3);
if (!mem.Contains(check3))
{
  mem.Add(check3);
  work.Enqueue(move3);
}
//Debluff
if (D > 0 && next.Ak > 0) 
{
  var akn = Math.Max(0, next.Ak-D);
  var move4 = new State() {Hd = hd - akn, Ad = next.Ad, Hk = next.Hk, Ak=akn, Turns = next.Turns+1};
  string check4 = GetCheck(move4);
  if (!mem.Contains(check4))
  {
      mem.Add(check4);
      work.Enqueue(move4);
  }
}
}
return int.MaxValue;
}

private static string GetCheck(State s)
{
return $"{s.Hd}#{s.Ad}#{s.Hk}#{s.Ak}";
}
*/
    }

    public class State
    {
        public int Hd { get; set; }
        public int Ad { get; set; }
        public int Hk { get; set; }
        public int Ak { get; set; }
        public int Turns { get; set; }
        public int Hd_c {get; private set;}
        public int B {get; private set;}
        public int D {get; private set;}

        public State(int hd, int ad, int hk, int ak, int b, int d)
        {
            this.Hd = hd;
            this.Ad = ad;
            this.Hk = hk;
            this.Ak = ak;
            this.Turns = 0;
            this.Hd_c = hd;
            this.B = b;
            this.D = d;
        }

        public State(State prev, char action)
        {
            this.B = prev.B;
            this.D = prev.D;
            this.Hd_c = prev.Hd_c;
            this.Turns = prev.Turns + 1;
            switch (action)
            {
                case 'a':
                    this.Hd = prev.Hd - prev.Ak;
                    this.Hk = prev.Hk - prev.Ad;
                    this.Ak = prev.Ak;
                    this.Ad = prev.Ad;
                    break;
                case 'b':
                    this.Hd = prev.Hd - prev.Ak;
                    this.Hk = prev.Hk;
                    this.Ak = prev.Ak;
                    this.Ad = prev.Ad + prev.B;
                    break;
                case 'c':
                    this.Hd = prev.Hd_c - prev.Ak;
                    this.Hk = prev.Hk;
                    this.Ak = prev.Ak;
                    this.Ad = prev.Ad;
                    break;
                case 'd':
                    int ak = Math.Max(prev.Ak - prev.D, 0);
                    this.Hd = prev.Hd - ak;
                    this.Hk = prev.Hk;
                    this.Ak = ak;
                    this.Ad = prev.Ad;
                    break;
                default:
                    throw new Exception("WRONG!");
            }
        }

        public override string ToString()
        {
            return $"{Hd}#{Ad}#{Hk}#{Ak}";
        }
    }
}
