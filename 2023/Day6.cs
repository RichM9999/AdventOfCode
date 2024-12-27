//https://adventofcode.com/2023/day/6
namespace AdventOfCode.Year2023
{
    class Day6
    {
        public void Run()
        {
            var start = DateTime.Now;

            var data = new List<string>
            {
                "Time:      57     72     69     92",
                "Distance: 291   1172   1176   2026"
            };

            List<(long time, long distance)> races = [];

            var timeData = data[0].Split(":")[1].Trim().Split(" ").Where(d => d.Length > 0).Select(d => long.Parse(d.Trim())).ToList();
            var distanceData = data[1].Split(":")[1].Trim().Split(" ").Where(d => d.Length > 0).Select(d => long.Parse(d.Trim())).ToList();

            for (var i = 0; i < timeData.Count; i++)
            {
                races.Add((timeData[i], distanceData[i]));
            }

            long answer = 1;

            foreach (var (time, distance) in races)
            {
                answer *= WaysToWin(time, distance);
            }

            Console.WriteLine();
            Console.WriteLine($"Product of ways to win: {answer}");
            // 160816
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            // Part 2
            start = DateTime.Now;

            //"Time:            57726992",
            //"Distance: 291117211762026"

            answer = WaysToWin(57726992, 291117211762026);
            Console.WriteLine();
            Console.WriteLine($"Ways to win single longer race: {answer}");
            // 46561107
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        long WaysToWin(long raceTime, long raceDistance)
        {
            long waysToWin = 0;

            for (long holdTime = 1; holdTime < raceTime - 1; holdTime++)
            {
                if (holdTime % (raceTime / 10) == 0)
                {
                    Console.Write(".");
                }

                if (((raceTime - holdTime) * holdTime) > raceDistance)
                {
                    waysToWin++;
                }
            }
            Console.Write("/");

            return waysToWin;
        }
    }
}