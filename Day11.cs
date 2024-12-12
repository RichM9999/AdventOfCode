//https://adventofcode.com/2024/day/11
namespace AdventOfCode
{
    class Day11
    {
        public void Run()
        {
            var stoneString = "4 4841539 66 5279 49207 134 609568 0";
            var start = DateTime.Now;

            var stones = stoneString.Split(' ').ToList().Select(s => long.Parse(s)).ToList();

            var blinks = 25;
            long sum = 0;
            var cache = new Dictionary<(long, int), long>();
            foreach (var stone in stones)
            {
                sum += RecursiveBlink(stone, blinks, cache);
            }
            Console.WriteLine($"Stones after 25 blinks: {sum}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;
            blinks = 75;
            sum = 0;
            cache = new Dictionary<(long, int), long>();
            foreach (var stone in stones)
            {
                sum += RecursiveBlink(stone, blinks, cache);
            }
            Console.WriteLine($"Stones after 75 blinks: {sum}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        public long RecursiveBlink(long stone, int blinks, Dictionary<(long, int), long> cache)
        {
            if (cache.ContainsKey((stone, blinks)))
            {
                return cache[(stone, blinks)];
            }

            if (blinks == 1)
            {
                return Blink(stone).Count();
            }

            var stones = Blink(stone);
            var results = stones.Select(stone => RecursiveBlink(stone, blinks - 1, cache));
            var count = results.Sum(countStones => countStones);

            cache[(stone, blinks)] = count;

            return count;
        }

        public long[] Blink(long stone)
        {
            if (stone == 0)
                return [1];

            var stoneString = stone.ToString();

            if (stoneString.Length % 2 == 0)
            {
                var left = stoneString.Substring(0, stoneString.Length / 2);
                var right = stoneString.Substring(stoneString.Length / 2, stoneString.Length / 2);
                return [long.Parse(left), long.Parse(right)];
            }

            return [ stone * 2024 ];
        }
    }
}
