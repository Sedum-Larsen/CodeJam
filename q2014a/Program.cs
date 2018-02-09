using System;
using System.Linq;
using System.Collections.Generic;

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
                var r = int.Parse(Console.ReadLine());
                Dictionary<int, int> cards = new Dictionary<int, int>();
                for(int ix = 1; ix < 5; ix++)
                {
                    var parts = Console.ReadLine().Split(' ').Select(w => int.Parse(w)).ToList();
                    if (ix == r)
                    {
                        foreach(var num in parts)
                        {
                            if (!cards.ContainsKey(num)) cards.Add(num, 0);
                            cards[num]++;
                        }    
                    }
                }
                r = int.Parse(Console.ReadLine());
                for(int ix = 1; ix < 5; ix++)
                {
                    var parts = Console.ReadLine().Split(' ').Select(w => int.Parse(w)).ToList();
                    if (ix == r)
                    {
                        foreach(var num in parts)
                        {
                            if (!cards.ContainsKey(num)) cards.Add(num, 0);
                            cards[num]++;
                        }    
                    }
                }
                var result = "";
                if (!cards.Values.Contains(2))
                    result = "Volunteer cheated!";
                else
                {
                    var temp = cards.Where(c => c.Value == 2).Select(c => c.Key).ToList();
                    if (temp.Count > 1)
                        result = "Bad magician!";
                    else
                        result = temp.First().ToString();
                }
                

                Console.WriteLine($"Case #{tc}: {result}");
            }
        }
    }
}


