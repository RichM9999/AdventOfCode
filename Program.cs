namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Day1:");
            var day1 = new Year2025.Day1();
            day1.Run();
            Console.WriteLine();

            Console.WriteLine("Day2:");
            var day2 = new Year2025.Day2();
            day2.Run();
            Console.WriteLine();

            Console.WriteLine("Day3:");
            var day3 = new Year2025.Day3();
            day3.Run();
            Console.WriteLine();
        }
    }
}