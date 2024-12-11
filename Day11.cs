//https://adventofcode.com/2024/day/11
namespace AdventOfCode
{
    class Day11
    {
        public void Run()
        {
            var stones = "4 4841539 66 5279 49207 134 609568 0";
            //stones = "125 17";

            var start = DateTime.Now;

            for (var i = 1; i <= 25; i++)
            {
                stones = Blink(stones);
                Console.Write($"{i}..");
            }
            
            Console.WriteLine();
            Console.WriteLine($"Stone count after 25 blinks: {stones.Split(' ').Length}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            for (var i = 1; i <= 50; i++)
            {
                stones = Blink(stones);
                Console.Write($"{i+25}..");
            }

            Console.WriteLine();
            Console.WriteLine($"Stone count after 75 blinks: {stones.Split(' ').Length}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        string Blink(string stones)
        {
            var stoneList = stones.Split(' ').ToList();

            var length = stoneList.Count;

            for (var i = 0; i < length; i++)
            {
                var stone = stoneList[i];

                if (long.Parse(stone) == 0)
                {
                    stoneList[i] = "1";
                    continue;
                }

                if (stone.Length % 2 == 0)
                {
                    var originalStone = stoneList[i];

                    stoneList[i] = long.Parse(originalStone.Substring(0, originalStone.Length / 2)).ToString();
                    stoneList.Insert(i+1, long.Parse(originalStone.Substring((originalStone.Length / 2))).ToString());
                    i++;
                    length++;
                    continue;
                }
                stoneList[i] = (long.Parse(stoneList[i]) * 2024).ToString();
            }

            return string.Join(' ', stoneList);
        }
    }
}
