using System;
using System.Collections.Generic;

namespace r1a2017c
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new System.IO.StreamReader("test.in"));
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
                var min_turns = int.MaxValue;

                var B_moves = FindB_opt(Ad, Hk, B);
                var movesBetweenCures = (Hd/Ak) - (Hd%Ak == 0?1:0);
                if (B_moves <= movesBetweenCures +1)
                {
                    min_turns = B_moves;
                }
                else
                {
                    if (D == 0)
                    {
                        min_turns = CalcTurns(B_moves, movesBetweenCures);
                    }
                    else
                    {
                        int init = (Hd - 1)/Ak;
                        var prevMoves = movesBetweenCures;
                        int taken = 0;
                        for(int ix = init + 1; ix < init + 21; ix++)
                        {
                            int steps = Hd/ix;
                            int corSteps = 0;
                            for(int iy = steps-1; iy < steps+2; iy++)
                            {
                                if ((Hd/(Ak-D*ix) - (Hd%(Ak-D*ix)==0?1:0)) > prevMoves)
                                {
                                    prevMoves = (Hd/(Ak-D*ix)) - (Hd%(Ak-D*ix)==0?1:0);
                                    taken += prevMoves + 1 + ((iy-corSteps) / (prevMoves - 1) * prevMoves);
                                    corSteps = iy;
                                    break;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine($"Case #{tc}: {(min_turns == int.MaxValue ? "IMPOSSIBLE":min_turns.ToString())} {B_moves} {movesBetweenCures}");
            }

        }

        private static int CalcTurns(int B_moves, int movesBetweenCures)
        {
            B_moves -= (movesBetweenCures + 1);
            if (movesBetweenCures > 1)
            {
                return movesBetweenCures + 1 + (B_moves / (movesBetweenCures - 1) * movesBetweenCures);
            }
            return int.MaxValue;
        }

        private static int FindB_opt(int ad, int hk, int b)
        {
            int res_moves = (hk/ad) + (hk % ad != 0? 1 : 0);
            if (b == 0) return res_moves;
            for(int ix = 1; ix <= hk-ad; ix++)
            {
                if (ad+ix*b > hk) return res_moves;
                var moves = ix + (hk/(ad+ix*b)) + (hk%(ad+ix*b) != 0 ? 1 : 0);
                if (moves < res_moves)
                {
                    res_moves = moves;
                }
            }
            return res_moves;
        }
    }
}