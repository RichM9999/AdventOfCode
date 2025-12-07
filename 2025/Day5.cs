//https://adventofcode.com/2025/day/5
namespace AdventOfCode.Year2025
{
    class Day5 : IDay
    {
        public void Run()
        {
            LoadData();

            var start = DateTime.Now;

            // 
            Console.WriteLine($"Part 1 answer: {Part1()}");
            // 
            Console.WriteLine($"Part 2 answer: {Part2()}");

            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        private long Part1()
        {
            return 0;
        }

        private long Part2()
        {
            return 0;
        }

        private void LoadData()
        {
        }
    }
}
